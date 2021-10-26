using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Tracking.Business.Services.Abstract;
using Vehicle.Tracking.Core.Utilities;
using Vehicle.Tracking.Entities.Models;
using Vehicle.Tracking.Entities.Models.Common;
using Vehicle.Tracking.Entities.Models.Filter;
using Vehicle.Tracking.Entities.Models.Request;
using Vehicle.Tracking.Entities.Models.Response;

namespace Vehicle.Tracking.Business.Services.Concrete
{
    public class TokenManager : ITokenManager
    {
        private readonly IOptionsSnapshot<BearerTokensOptions> _configuration;
        private IRightManager _rightManager;
        private IUserManager _userManager;

        public TokenManager(IOptionsSnapshot<BearerTokensOptions> configuration, IRightManager rightManager, IUserManager userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _rightManager = rightManager;
        }

        public async Task<JwtTokensData> CreateJwtTokensAsync(User user)
        {
            var (accessToken, claims) = await CreateAccessTokenAsync(user);
            var (refreshTokenValue, refreshTokenSerial) = CreateRefreshToken();
            return new JwtTokensData
            {
                AccessToken = accessToken,
                RefreshToken = refreshTokenValue,
                RefreshTokenSerial = refreshTokenSerial,
                Claims = claims
            };
        }

        public UserResponse FindToken(string refreshTokenValue)
        {
            if (string.IsNullOrWhiteSpace(refreshTokenValue))
                return null;

            var refreshTokenSerial = GetRefreshTokenSerial(refreshTokenValue);

            if (string.IsNullOrWhiteSpace(refreshTokenSerial))
                return null;

            var request = new UserRequest { Filter = new UserFilter { RefreshToken = refreshTokenValue } };

            var refreshTokenIdHash = Cryptographer.GetSha256Hash(refreshTokenSerial);

            return _userManager.GetByFilter(request);
        }

        public string GetRefreshTokenSerial(string refreshTokenValue)
        {
            if (string.IsNullOrWhiteSpace(refreshTokenValue))
            {
                return null;
            }

            ClaimsPrincipal decodedRefreshTokenPrincipal = null;
            try
            {
                decodedRefreshTokenPrincipal = new JwtSecurityTokenHandler().ValidateToken(
                    refreshTokenValue,
                    new TokenValidationParameters
                    {
                        RequireExpirationTime = true,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Value.Key)),
                        ValidateIssuerSigningKey = true, // verify signature to avoid tampering
                        ValidateLifetime = true, // validate the expiration
                        ClockSkew = TimeSpan.Zero // tolerance for the expiration date
                    },
                    out _
                );
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return decodedRefreshTokenPrincipal?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.SerialNumber)?.Value;
        }

        #region Helper

        private async Task<(string AccessToken, IEnumerable<Claim> Claims)> CreateAccessTokenAsync(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Cryptographer.CreateCryptographicallySecureGuid().ToString(), ClaimValueTypes.String, _configuration.Value.Issuer),
                new Claim(JwtRegisteredClaimNames.Iss, _configuration.Value.Issuer, ClaimValueTypes.String, _configuration.Value.Issuer),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64, _configuration.Value.Issuer),
                new Claim("UserId", user.Id.ToString(), ClaimValueTypes.String, _configuration.Value.Issuer),
                //new Claim("RegNum", user.RegNum, ClaimValueTypes.String, _configuration.Value.Issuer),
                new Claim("Email", user.Email, ClaimValueTypes.String, _configuration.Value.Issuer),
                new Claim("DisplayName", user.Name+" "+ user.Surname, ClaimValueTypes.String, _configuration.Value.Issuer)
            };

            // add roles TODO Emre
            var roles = await _rightManager.FindUserRolesAsync(user.Id);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Role.Name));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Value.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var now = DateTime.UtcNow;
            var token = new JwtSecurityToken(
                issuer: _configuration.Value.Issuer,
                audience: _configuration.Value.Audience,
                claims: claims,
                notBefore: now,
                expires: now.AddDays(_configuration.Value.AccessTokenExpirationDates),
                signingCredentials: creds);
            return (new JwtSecurityTokenHandler().WriteToken(token), claims);
        }

        private (string RefreshTokenValue, string RefreshTokenSerial) CreateRefreshToken()
        {
            var refreshTokenSerial = Cryptographer.CreateCryptographicallySecureGuid().ToString().Replace("-", "");
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Cryptographer.CreateCryptographicallySecureGuid().ToString(), ClaimValueTypes.String, _configuration.Value.Issuer),
                new Claim(JwtRegisteredClaimNames.Iss, _configuration.Value.Issuer, ClaimValueTypes.String, _configuration.Value.Issuer),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64, _configuration.Value.Issuer),
                new Claim(ClaimTypes.SerialNumber, refreshTokenSerial, ClaimValueTypes.String, _configuration.Value.Issuer)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Value.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var now = DateTime.UtcNow;
            var token = new JwtSecurityToken(
                issuer: _configuration.Value.Issuer,
                audience: _configuration.Value.Audience,
                claims: claims,
                notBefore: now,
                expires: now.AddDays(_configuration.Value.RefreshTokenExpirationDates),
                signingCredentials: creds);
            var refreshTokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            return (refreshTokenValue, refreshTokenSerial);
        }

        #endregion
    }
}

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Tracking.Business.Handlers.Authorizations.Queries;
using Vehicle.Tracking.Business.Services.Abstract;
using Vehicle.Tracking.Core.Utilities;
using Vehicle.Tracking.DataAccess.Abstract;
using Vehicle.Tracking.Entities.Models;
using Vehicle.Tracking.Entities.Models.Common;
using Vehicle.Tracking.Entities.Models.Request;
using Vehicle.Tracking.Entities.Models.Response;

namespace Vehicle.Tracking.Business.Services.Concrete
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;
        private readonly IRightManager _rightManager;
        private readonly IOptionsSnapshot<BearerTokensOptions> _configuration;
        private readonly IAntiForgeryCookieManager _antiForgeryCookieManager;

        public UserManager(IUserRepository userRepository, IOptionsSnapshot<BearerTokensOptions> configuration, IRightManager rightManager, IAntiForgeryCookieManager antiForgeryCookieManager)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _rightManager = rightManager;
            _antiForgeryCookieManager = antiForgeryCookieManager;
        }

        public Task<User> AddAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _userRepository.Get(x => x.Email == model.Email && x.Password == model.Password);

            if (user == null) return null;

            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }
        private string generateJwtToken(User user)
        {
       
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.Value.Key);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Cryptographer.CreateCryptographicallySecureGuid().ToString(), ClaimValueTypes.String, _configuration.Value.Issuer),
                new Claim(JwtRegisteredClaimNames.Iss, _configuration.Value.Issuer, ClaimValueTypes.String, _configuration.Value.Issuer),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64, _configuration.Value.Issuer),
                new Claim("UserId", user.Id.ToString(), ClaimValueTypes.String, _configuration.Value.Issuer),
                new Claim("Email", user.Email, ClaimValueTypes.String, _configuration.Value.Issuer),
                new Claim("DisplayName", user.Name+" "+ user.Surname, ClaimValueTypes.String, _configuration.Value.Issuer)
            };
            var roles = _rightManager.FindUserRoles(user.Id).ToList();
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Role.Name));
            }
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            _antiForgeryCookieManager.RegenerateAntiForgeryCookies(claims);
            return tokenHandler.WriteToken(token);
        }
        public Task DeleteAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetAsync(LoginUserQuery query)
        {
            return await _userRepository.GetAsync(x => x.Email == query.Email && x.Password == query.Password);

        }

        public UserResponse GetByFilter(UserRequest request)
        {
            var response = new UserResponse() { Entity = new User() };
            var query = _userRepository.GetAllInclude(null, new string[] { "Rights", "Rights.Role" });

            if (!string.IsNullOrEmpty(request.Filter.RefreshToken))
            {
                query = query.Where(x => x.RefreshToken == request.Filter.RefreshToken);
            }

            if (!string.IsNullOrEmpty(request.Filter.Email))
            {
                query = query.Where(x => x.Email == request.Filter.Email);
            }

            if (!string.IsNullOrEmpty(request.Filter.Password))
            {
                query = query.Where(x => x.Password == request.Filter.Password);
            }

            response.Entity = query.FirstOrDefault();


            return response;
        }

        public Task<User> UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }
    }
}

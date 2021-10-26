using System.Threading.Tasks;
using Vehicle.Tracking.Entities.Models;
using Vehicle.Tracking.Entities.Models.Common;
using Vehicle.Tracking.Entities.Models.Response;

namespace Vehicle.Tracking.Business.Services.Abstract
{
    public interface ITokenManager
    {
        Task<JwtTokensData> CreateJwtTokensAsync(User user);

        UserResponse FindToken(string refreshTokenValue);

        string GetRefreshTokenSerial(string refreshTokenValue);
    }
}

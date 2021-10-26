using System.Collections.Generic;
using System.Security.Claims;

namespace Vehicle.Tracking.Business.Services.Abstract
{
    public interface IAntiForgeryCookieManager
    {
        void RegenerateAntiForgeryCookies(IEnumerable<Claim> claims);

        void DeleteAntiForgeryCookies();
    }
}

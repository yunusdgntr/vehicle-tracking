using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Tracking.Business.Services.Abstract;

namespace Vehicle.Tracking.Business.Services.Concrete
{
   
    public class AntiForgeryCookieManager : IAntiForgeryCookieManager
    {
        private const string XsrfTokenKey = "XSRF-TOKEN";

        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IAntiforgery _antiforgery;
        private readonly IOptions<AntiforgeryOptions> _antiforgeryOptions;

        public AntiForgeryCookieManager(IHttpContextAccessor contextAccessor, IAntiforgery antiforgery, IOptions<AntiforgeryOptions> antiforgeryOptions)
        {
            _contextAccessor = contextAccessor;
            _antiforgery = antiforgery;
            _antiforgeryOptions = antiforgeryOptions;
        }

        public void RegenerateAntiForgeryCookies(IEnumerable<Claim> claims)
        {
            var httpContext = _contextAccessor.HttpContext;
            httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme));
            var tokens = _antiforgery.GetAndStoreTokens(httpContext);
            httpContext.Response.Cookies.Append(
                  key: XsrfTokenKey,
                  value: tokens.RequestToken,
                  options: new CookieOptions
                  {
                      HttpOnly = false 
                  });
        }

        public void DeleteAntiForgeryCookies()
        {
            var cookies = _contextAccessor.HttpContext.Response.Cookies;
            cookies.Delete(_antiforgeryOptions.Value.Cookie.Name);
            cookies.Delete(XsrfTokenKey);
        }
    }
}

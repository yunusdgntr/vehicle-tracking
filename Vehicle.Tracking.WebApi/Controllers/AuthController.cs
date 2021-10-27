using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Vehicle.Tracking.Business.Handlers.Authorizations.Queries;
using Vehicle.Tracking.Business.Services.Abstract;
using Vehicle.Tracking.Entities.Enums;
using Vehicle.Tracking.Entities.Models.Filter;
using Vehicle.Tracking.Entities.Models.Request;

namespace Vehicle.Tracking.WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserManager _userManager;
       

        public AuthController(IConfiguration configuration, IUserManager userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserQuery loginModel)
        {
            if (loginModel == null)
            {
                return BadRequest("User is not set.");
            }

            var user = await _userManager.GetAsync(loginModel);

            if (user == null || user.Status != (int)StatusType.Active)
            {
                return Unauthorized();
            }
            var s = _userManager.Authenticate(new AuthenticateRequest { Email = loginModel.Email, Password = loginModel.Password });
       
            return Ok(s);
        }
    }
}

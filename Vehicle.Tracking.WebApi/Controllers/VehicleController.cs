using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Vehicle.Tracking.Business.Services.Abstract;

namespace Vehicle.Tracking.WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleManager _vehicleManager; 
 
        public VehicleController(IVehicleManager vehicleManager)
        {
            _vehicleManager = vehicleManager;
        }


        [AllowAnonymous]
        [HttpGet("get")]
        public IActionResult Get()
        {
            return  Ok("This is public endpoint!");
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("getadmintest")]
        public IActionResult GetAdminTest()
        {
            return Ok("This is only for admin!");
        }

        [Authorize(Roles = "Admin,Customer")]
        [HttpGet("getcustomertest")]
        public IActionResult GetCustomerTest()
        {
            return Ok("This is only for Customer!");
        }

        /// <summary>
        /// Vehicle info can be updated by Admin
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        [HttpPut("update")]
        [Authorize(Roles ="Admin")]
        public IActionResult Update([FromBody] Entities.Models.Vehicle vehicle)
        {
            var result = _vehicleManager.Update(vehicle);
            if (result!=null)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}

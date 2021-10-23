using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vehicle.Tracking.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        [HttpGet("gettest")]
        public async Task<IActionResult> Get()
        {
            return  Ok("Test");
        }
    }
}

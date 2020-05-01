using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Managers;
using API.Models.json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Gateway_Controllers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceDiscoveryController : ControllerBase
    {
        private readonly ServiceDiscoveryManager _serviceDiscoveryManager;
        public ServiceDiscoveryController(ServiceDiscoveryManager serviceDiscoveryManager)
        {
            _serviceDiscoveryManager = serviceDiscoveryManager;
        }

        [HttpGet("DisplayServices/{clientId}")]
        [Produces("application/json")]
        public IActionResult DisplayServices(string clientId)

        {
            try
            {
                return Ok(_serviceDiscoveryManager.GetAvailableServices(clientId));
            }
            catch{
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
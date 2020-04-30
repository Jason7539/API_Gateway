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
    public class ServiceDisplayController : ControllerBase
    {
        private readonly ServiceDisplayManager _serviceDisplayManager;
        public ServiceDisplayController(ServiceDisplayManager serviceDisplayManager)
        {
            _serviceDisplayManager = serviceDisplayManager;
        }

        [HttpGet("DisplayServices")]
        [Produces("application/json")]
        public IActionResult DisplayServices(ServiceDisplayPost serviceDisplayPost)
        {
            return Ok(_serviceDisplayManager.GetAvailableServices(serviceDisplayPost));
        }
    }
}
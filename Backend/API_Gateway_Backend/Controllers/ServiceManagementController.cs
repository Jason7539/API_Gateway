using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Managers;
using API.Models.json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Gateway_Controllers.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceManagementController : ControllerBase
    {

        private readonly ServiceManagementManager _serviceManagementManager;

        public ServiceManagementController(ServiceManagementManager serviceManagementManager)
        {
            _serviceManagementManager = serviceManagementManager;
        }


        [HttpPost("CreateService")]
        [Consumes("application/json")]
        public IActionResult CreateServicePost(CreateServicePost createServicePost)
        {
            return Ok(_serviceManagementManager.CreateService(createServicePost));
        }

        [HttpGet("GetTeams")]
        [Produces("application/json")]
        public IActionResult GetTeams()
        {
            return Ok(_serviceManagementManager.GetTeamsUsername());
        }

    }
}
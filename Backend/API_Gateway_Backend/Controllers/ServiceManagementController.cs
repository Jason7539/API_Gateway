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

        [HttpGet("GetOwnedServicePagination/{clientId}")]
        [Produces("application/json")]
        public IActionResult GetOwnedServicePagination(string clientId)
        {
            return Ok(_serviceManagementManager.GetOwnedServicePagination(clientId));
        }

        // Get owned services
        // Check that they are authorized to do this.
        [HttpGet("GetOwnedServices/{clientId}/{pagination}")]
        [Produces("application/json")]
        public IActionResult GetOwnedServices(string clientId, int pagination)
        {
            return Ok(_serviceManagementManager.GetOwnedServices(clientId, pagination));
        }

        // Delete Services and configurations 
        [Authorize(Policy = "IsOwner")]
        [HttpDelete("DeleteService/{endpoint}")]
        [Produces("application/json")]
        public IActionResult DeleteService(string endpoint)
        {
            return Ok(_serviceManagementManager.DeleteService(endpoint));
        }

        // Change Service Privacy.
        [Authorize(Policy = "IsOwner")]
        [HttpPatch("UpdateServicePrivacy/{endpoint}")]
        [Produces("application/json")]
        public IActionResult UpdateServicePrivacy(UpdateServicePatch updateServicePatch)
        {
            return Ok(_serviceManagementManager.UpdateServicePrivacy(updateServicePatch));
        }

        // Get Who a service is open to 
        [Authorize(Policy = "IsOwner")]
        [HttpGet("GetAllowedUsers/{endpoint}")]
        [Produces("application/json")]
        public IActionResult GetAllowedConfigurationUsers(string endpoint)
        {
            return Ok(_serviceManagementManager.GetAllowedConfigurationUsers(endpoint));
        }


    }
}
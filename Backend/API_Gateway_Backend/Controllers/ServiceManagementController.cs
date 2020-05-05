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
            try
            {
                return Ok(_serviceManagementManager.CreateService(createServicePost));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("GetTeams")]
        [Produces("application/json")]
        public IActionResult GetTeams()

        {
            try
            {
                return Ok(_serviceManagementManager.GetTeamsUsername());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("GetOwnedServicePagination/{clientId}")]
        [Produces("application/json")]
        public IActionResult GetOwnedServicePagination(string clientId)
        {
            try
            {
                return Ok(_serviceManagementManager.GetOwnedServicePagination(clientId));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("GetOwnedServices/{clientId}/{pagination}")]
        [Produces("application/json")]
        public IActionResult GetOwnedServices(string clientId, int pagination)
        {
            try
            {
                return Ok(_serviceManagementManager.GetOwnedServices(clientId, pagination));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // Delete Services and configurations 
        [Authorize(Policy = "IsOwner")]
        [HttpDelete("DeleteService/{endpoint}")]
        [Produces("application/json")]
        public IActionResult DeleteService(string endpoint)
        {
            try
            {
                return Ok(_serviceManagementManager.DeleteService(endpoint));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // Change Service Privacy.
        [Authorize(Policy = "IsOwner")]
        [HttpPatch("UpdateServicePrivacy/{endpoint}")]
        [Produces("application/json")]
        public IActionResult UpdateServicePrivacy(UpdateServicePatch updateServicePatch)
        {
            try
            {
                return Ok(_serviceManagementManager.UpdateServicePrivacy(updateServicePatch));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // Get Who a service is open to 
        [Authorize(Policy = "IsOwner")]
        [HttpGet("GetAllowedUsers/{endpoint}")]
        [Produces("application/json")]
        public IActionResult GetAllowedConfigurationUsers(string endpoint)
        {
            try
            {
                return Ok(_serviceManagementManager.GetAllowedConfigurationUsers(endpoint));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


    }
}
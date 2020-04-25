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
    public class ServiceManagementController : ControllerBase
    {

        private readonly ServiceManagementManager _serviceManagementManager;

        public ServiceManagementController(ServiceManagementManager serviceManagementManager)
        {
            _serviceManagementManager = serviceManagementManager;
        }


        [HttpPost("CreateService")]
        public IActionResult CreateServicePost(CreateServicePost createServicePost)
        {


            return Ok(_serviceManagementManager.CreateService(createServicePost));
        }

    }
}
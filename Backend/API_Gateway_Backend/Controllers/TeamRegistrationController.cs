using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models.gateway;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Models.json;
using API.Managers;

namespace API_Gateway_Controllers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamRegistrationController : ControllerBase
    {
        private readonly ApiGatewayContext _apiGatewayContext;
        private readonly TeamRegistrationManager _teamRegistrationManager;

        public TeamRegistrationController(ApiGatewayContext apiGatewayContext, TeamRegistrationManager teamRegistrationManager)
        {
            _apiGatewayContext = apiGatewayContext;
            _teamRegistrationManager = teamRegistrationManager;
        }

        [HttpPost("CreateTeam")]
        [Consumes("application/json")]
        public IActionResult RegisterTeam(TeamRegisterPost postInfo)
        {
            try
            {
                return Ok(_teamRegistrationManager.CreateTeamAccount(postInfo));

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
    }
}
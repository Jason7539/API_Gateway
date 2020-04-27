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
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly TeamLoginManager _teamLoginManager;
        public AuthenticateController(TeamLoginManager teamLoginManager)
        {
            _teamLoginManager = teamLoginManager;
        }

        [HttpPost("Login")]
        [Consumes("application/json")]
        public IActionResult TeamLogin(TeamLoginPost postInfo)
        {
            try
            {
                return Ok(_teamLoginManager.TeamLogin(postInfo));
            }
            catch
            {
            return NotFound();
            }
        }

        [Authorize(Policy = "IsOwner")]
        [HttpGet("secure/{username}")]
        public IActionResult Protected(string username)
        {
            return Ok("hello honey");
        }


    }
}
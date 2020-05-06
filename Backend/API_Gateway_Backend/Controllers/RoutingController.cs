using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Markup;
using API.Managers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Protocols.WSIdentity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Gateway_Controllers.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class RoutingController : Controller
    {
        private readonly RoutingManager _manager;
        public RoutingController(RoutingManager manager)
        {
            _manager = manager;
        }
      

        /// <summary>
        /// Catch all Route To intercept all traffic to this controller.
        /// Uses the manager contained in the constructor to return the results of its query
        /// </summary>
        /// <param name="actionFromUrl"></param>
        /// <returns></returns>
        // api/<controller>/
        [RequireHttps]
        [Route("/{**catchAll}")]
        public IActionResult Index()
        {
            try
            {
                return Ok(_manager.RouteExecute(Request));
            }
            catch (UriFormatException)
            {
                return NotFound();
            }
            catch(InvalidInputException)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            catch(InvalidCastException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch(UnauthorizedAccessException)
            {
                return Unauthorized("Unauthorized Access");
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

    }
}

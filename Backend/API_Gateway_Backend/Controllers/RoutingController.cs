using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Markup;
using API.Managers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Protocols.WSIdentity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Gateway_Controllers.Controllers
{
    [Route("api/[controller]")]
    public class RoutingController : Controller
    {
        public RoutingManager Manager { get; set; }
        public RoutingController(RoutingManager manager)
        {
            Manager = manager;
        }
      
        // api/<controller>/
        [RequireHttps]
        [Route("/{**catchAll}")]
        public IActionResult Index(String actionFromUrl)
        {
            //actionFromUrl will contain all url after the api/controller/
            var bodyString = Request.Body.ToString();
            var httpMethod = Request.Method;
            StringValues authToken;
            Request.Headers.TryGetValue("Authorization", out authToken);
            if (authToken.Count == 0)
            {
                return Unauthorized( "Unauthorized Access");
            }

            try
            {

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

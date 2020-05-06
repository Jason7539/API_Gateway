using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Claims;
using Microsoft.IdentityModel.Protocols.WSIdentity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Mvc;

namespace API.Managers
{
    /// <summary>
    /// Class for Managing Different Router Calls
    /// </summary>
    public class RoutingManager
    {
        ILLBuilder Builder { get; set; }
        ILLRouter Router { get; set; }
        JWTService AuthService { get; set; }
        public RoutingManager(JWTService authService, ILLBuilder builder) 
        {
            AuthService = AuthService;
            Builder = builder;
        }

        /// <summary>
        /// This method accepts the httprequest object from the controller and attempts to both build and execute the route that corresponds with the request
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string RouteExecute(HttpRequest message)
        {
            //actionFromUrl will contain all url after the api/controller/         
            StringValues authToken;
            message.Headers.TryGetValue("Authorization", out authToken);
            if (authToken.Count == 0)
            {
                throw new UnauthorizedAccessException();
            }
            var properToken = authToken[0].ToString();

            //various catches in order to log more specific exceptions
            try
            {
                //grabs token's scope claim and puts it toString
                var scope = AuthorizeJwtToken(properToken);
                Router = Builder.Build(message, scope);

                var returnedStringJson= Router.Execute();        
                ///TODO : return this json all the way up, set it up as a httpresponsemessage
            }
            catch(Exception e)
            {
                throw;
            }
            return null;


        }

        /// <summary>
        /// this private method separates the logic of finding the scope inside of the jwtToken
        /// </summary>
        /// <param name="authContext"></param>
        /// <returns></returns>
        private string AuthorizeJwtToken(string authContext)
        {
            //uses token handler to generate jwtToken from the given authorization header
            var handler = new JwtSecurityTokenHandler();
            var properToken = handler.ReadJwtToken(authContext);
            var scope = properToken.Claims.Select(c => new { c.Type, c.Value }).Where(c => c.Type == "scope").Select(c => c.Value).ToString();
            if(scope is null)
            {
                throw new UnauthorizedAccessException("Invalid Token");
            }
            return scope;

        }


    }
}

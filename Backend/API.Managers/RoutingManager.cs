using API.Services;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Managers
{
    /// <summary>
    /// Class for Managing Different Router Calls
    /// </summary>
    public class RoutingManager
    {
        public ILLBuilder Builder { get; set; }
        public ILLRouter Router { get; set; }
        public RoutingManager() { }

        public void RouteExecute(AuthorizationHandlerContext authContext, string serviceConfigID, string callbackUrl)
        {

            ///TODO: pull from the service configuration
            ILLBuilder routeBuilder = new LLFireForgetBuilder();
            ILLRouter route = routeBuilder.Build(authContext, serviceConfigID);

        }

    }
}

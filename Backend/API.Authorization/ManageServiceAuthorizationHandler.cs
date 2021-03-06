﻿using API.Models.gateway;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace API.Authorization
{
    public class ManageServiceAuthorizationHandler : AuthorizationHandler<ManageServiceRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApiGatewayContext _apiGatewayContext; 

        public ManageServiceAuthorizationHandler(IHttpContextAccessor httpContextAccessor, ApiGatewayContext apiGatewayContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _apiGatewayContext = apiGatewayContext;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ManageServiceRequirement requirement)
        {
            // Grab the route they are trying to access. 
            var routeData = _httpContextAccessor.HttpContext.GetRouteData();

            // Grab the resource they are trying to do manipulate. If value is missing return null.
            var resource = routeData?.Values["endpoint"].ToString();

            // Look into the token and grabs its scope. If scope is missing return null.
            var scopeclaim = context.User.FindFirst("Scope")?.Value;

            // Authorization fails if token is missing claims or when url is not well formed.
            if (scopeclaim == null || resource == null)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            // Check if the owner owns the service he is trying to manipulate.
            // Join Teams with service and filter to find if they own the service.
            var queryResult = from team in _apiGatewayContext.Team
                              join service in _apiGatewayContext.Service on team.ClientId equals service.Owner
                              where resource == service.Endpoint && scopeclaim == team.ClientId
                              select team.Username;
            
            // If count == 0. The client does not own that service.
            var ownerList = queryResult.ToList().Count();
            
            if(ownerList == 0)
            {
                context.Fail();
            }
            else
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

    public class ManageServiceRequirement: IAuthorizationRequirement
    {

    }
}

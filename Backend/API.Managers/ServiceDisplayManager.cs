using System;
using System.Collections.Generic;
using System.Text;
using API.Models.gateway;
using API.Models.json;
using API.Services;
using Microsoft.EntityFrameworkCore.Query;

namespace API.Managers
{
   public class ServiceDisplayManager
    {
        private readonly ServiceDisplayService _serviceDisplayService;
        private readonly JWTService _JWTService;

        public ServiceDisplayManager(ServiceDisplayService serviceDisplayService, JWTService jwtService)
        {
            _serviceDisplayService = serviceDisplayService;
            _JWTService = jwtService;
        }
        public ICollection<ServiceDisplayResp> GetAvailableServices(ServiceDisplayPost serviceDisplayPost)
        {
            var clientId = serviceDisplayPost.ClientId;
            var accessToken = serviceDisplayPost.AccessToken;
            var ifAccessTokenValid = false;
            var ifClientExist = false;
            ICollection<ServiceDisplayResp> resultSet = null; 

            if (String.IsNullOrWhiteSpace(clientId) || String.IsNullOrWhiteSpace(accessToken)||
                clientId.Length != Int32.Parse(Environment.GetEnvironmentVariable("APIKeyInputLength", EnvironmentVariableTarget.User)))
                return null;

            ifAccessTokenValid = new JWTService().ValidateHmacSignedJWTToken(accessToken);

            if (ifAccessTokenValid)
                ifClientExist = _serviceDisplayService.IfClientExist(clientId);

            if (ifClientExist)
                resultSet = _serviceDisplayService.LoadData(clientId);
            
            return resultSet;
        }
    }
}

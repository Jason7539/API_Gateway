using System;
using System.Collections.Generic;
using System.Text;
using API.Models.gateway;
using API.Models.json;
using API.Services;

namespace API.Managers
{
   public class ServiceDiscoveryManager
    {
        private readonly ServiceDiscoveryService _serviceDiscoveryService;

        public ServiceDiscoveryManager(ServiceDiscoveryService serviceDiscoveryService)
        {
            _serviceDiscoveryService = serviceDiscoveryService;
        }
        public ICollection<ServiceDisplayResp> GetAvailableServices(string clientId)
        {
            ICollection<ServiceDisplayResp> resultSet = null; 

            if (String.IsNullOrWhiteSpace(clientId)||
                clientId.Length != Int32.Parse(Environment.GetEnvironmentVariable("APIKeyInputLength", EnvironmentVariableTarget.User)))
                return null;


            if (_serviceDiscoveryService.IfClientExist(clientId))
                resultSet = _serviceDiscoveryService.LoadData(clientId);
            
            return resultSet;
        }
    }
}

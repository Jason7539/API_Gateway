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

        /// <summary>
        /// Get a list of available services for a client based on the clientId
        /// </summary>
        /// <param name="clientId">the clientId from front end that login to the system to view services</param>
        /// <returns>resultSet a list of services that are open to the client based on clientId</returns>
        public ICollection<ServiceDisplayResp> GetAvailableServices(string clientId)
        {
            ICollection<ServiceDisplayResp> resultSet = null; 

            if (String.IsNullOrWhiteSpace(clientId)||
                clientId.Length != Int32.Parse(Environment.GetEnvironmentVariable("APIKeyInputLength", EnvironmentVariableTarget.User)))
                return null;


            if (_serviceDiscoveryService.IfClientExist(clientId))
                resultSet = _serviceDiscoveryService.GetServices(clientId);
            
            return resultSet;
        }
    }
}

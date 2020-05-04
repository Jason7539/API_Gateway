using System;
using System.Collections.Generic;
using API.DAL;
using API.Models.gateway;
using API.Models.json;

namespace API.Services
{
    public class ServiceDiscoveryService
    {
        private readonly ServiceDiscoveryDAO _serviceDisplayDAO;

        public ServiceDiscoveryService(ApiGatewayContext dbContext)
        {
            _serviceDisplayDAO = new ServiceDiscoveryDAO(dbContext);
        }

        /// <summary>
        /// Get a list of available services for a client based on the clientId
        /// </summary>
        /// <param name="clientId">the clientId from front end that login to the system to view services</param>
        /// <returns>resultSet a list of services that are open to the client based on clientId</returns>
        public ICollection<ServiceDisplayResp> GetServices(string clientId)
        {
            ICollection<ServiceDisplayResp> resultSet = null;
            if (!String.IsNullOrWhiteSpace(clientId))
            {
                resultSet = _serviceDisplayDAO.GetServices(clientId);

            }
            return resultSet;
        }

        /// <summary>
        /// Check if a client exist in the database
        /// </summary>
        /// <param name="clientId">the clientId from front end that login to the system to view services</param>
        /// <returns>bool if the clientId can be found in the database</returns>
        public bool IfClientExist(string clientId)
        {
            var ifClientExist = false;
            if (!String.IsNullOrWhiteSpace(clientId))
                ifClientExist = _serviceDisplayDAO.IfClientExist(clientId);

            return ifClientExist;
        }
    }
}
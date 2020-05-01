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

        //Load all data beside the team that sent request
        public ICollection<ServiceDisplayResp> LoadData(string clientId)
        {
            ICollection<ServiceDisplayResp> resultSet = null;
            if (!String.IsNullOrWhiteSpace(clientId))
            {
                resultSet = _serviceDisplayDAO.GetAllData(clientId);

            }

            return resultSet;
        }

        public bool IfClientExist(string clientId)
        {
            var ifClientExist = false;
            if (!String.IsNullOrWhiteSpace(clientId))
                ifClientExist = _serviceDisplayDAO.IfClientExist(clientId);

            return ifClientExist;
        }
    }
}
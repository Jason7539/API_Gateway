using System;
using System.Collections.Generic;
using API.DAL;
using API.Models.gateway;
using API.Models.json;

namespace API.Services
{
    public class ServiceDisplayService
    {
        private readonly IServiceDisplayDAO _serviceDisplayDAO;

        public ServiceDisplayService(ApiGatewayContext dbContext)
        {
            _serviceDisplayDAO = new SqlServiceDisplayDAO(dbContext);
        }

        //Load all data beside the team that sent request
        public ICollection<ServiceDisplayResp> LoadData(string clientId)
        {
            ICollection<ServiceDisplayResp> resultSet = null;
            //Make sure input apikey is not empty and meet the length requirement
            if (!String.IsNullOrWhiteSpace(clientId))
            {
                resultSet = _serviceDisplayDAO.GetAllData(clientId);

            }

            return resultSet;
        }

        public bool IfClientExist(string clientId)
        {
            if (!String.IsNullOrWhiteSpace(clientId))
                return _serviceDisplayDAO.IfClientExist(clientId);

            return false;
        }
    }
}
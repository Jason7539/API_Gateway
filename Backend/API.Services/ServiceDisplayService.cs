using System;
using System.Collections.Generic;
using API.Models.gateway;
using DataAccessLayer;

namespace ServiceLayer
{
    public class ServiceDisplayService
    {
        private readonly ApiGatewayContext _dbContext;

        public ServiceDisplayService(ApiGatewayContext dbContext)
        {
            _dbContext = dbContext;

        }
        //TODO: Validate the Token


        //Load all data beside the team that sent request
        public ICollection<Service> LoadData(string teamName)
        {
            //Make sure input apikey is not empty and meet the length requirement
            if (String.IsNullOrWhiteSpace(teamName) || teamName.Length != Int32.Parse(Environment.GetEnvironmentVariable("APIKeyInputLength", EnvironmentVariableTarget.User)))
                return null;

            IServiceDisplayDAO dao = new SqlServiceDisplayDAO(_dbContext);
            if (dao.IfTeamExist(teamName))
                return dao.GetAllData(teamName);
            else
                return null;

        }


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using API.Models.gateway;

namespace API.DAL
{
    public class SqlServiceDisplayDAO : IServiceDisplayDAO
    {
        private readonly ApiGatewayContext _dbContext;

        public SqlServiceDisplayDAO(ApiGatewayContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Gather all data from the database about registered services
        public ICollection<Service> GetAllData(string teamName)
        {
            if (String.IsNullOrWhiteSpace(teamName) || teamName.Length != Int32.Parse(Environment.GetEnvironmentVariable("APIKeyInputLength", EnvironmentVariableTarget.User)))
                return null;

            //IfPublic 0 = private 1 = public
            using (_dbContext)
            {
                if (!_dbContext.Configuration.Any())
                    return null;
                else
                {
                    var endPointList = _dbContext.Configuration.SingleOrDefault(a => a.OpenTo == teamName).EndPoint;
                        return _dbContext.Service.Where(a => endPointList.Contains(a.Endpoint)).OrderBy(a => a.Id).ToList();
                }
            }
        }

        //Method that will retrive result
        public bool IfTeamExist(string teamName)
        {
            if (String.IsNullOrWhiteSpace(teamName) || teamName.Length != Int32.Parse(Environment.GetEnvironmentVariable("APIKeyInputLength", EnvironmentVariableTarget.User)))
                return false;
            using (_dbContext)
            {
                if (_dbContext.Team.Any(a => a.ClientId == teamName))
                    return true;
                else
                    return false;
            }
        }
    }
}
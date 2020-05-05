using System;
using System.Collections.Generic;
using System.Linq;
using API.Models.gateway;
using API.Models.json;

namespace API.DAL
{
    public class ServiceDiscoveryDAO 
    {
        private readonly ApiGatewayContext _dbContext;

        public ServiceDiscoveryDAO(ApiGatewayContext dbContext)
        {
            _dbContext = dbContext;
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
                if (_dbContext.Configuration.Where(a => a.OpenTo == clientId).Any())
                {

                    var endPointList = _dbContext.Configuration.Where(a => a.OpenTo == clientId).Select(a => a.EndPoint).Distinct();
                    var dataSet = _dbContext.Service.Join(_dbContext.Team, service => service.Owner,
                    team => team.ClientId, (service, team) => new
                    {
                        team.Username,
                        service.Endpoint,
                        service.Input,
                        service.Output,
                        service.Dataformat,
                        service.Description
                    }
                     ).Where(a => endPointList.Contains(a.Endpoint)).OrderBy(a => a.Username).ToList();

                    resultSet = new List<ServiceDisplayResp>();

                    foreach (var service in dataSet)
                    {
                        resultSet.Add(new ServiceDisplayResp()
                        {
                            Endpoint = service.Endpoint,
                            Username = service.Username,
                            Input = service.Input,
                            Output = service.Output,
                            Dataformat = service.Dataformat,
                            Description = service.Description
                        });


                    }
                }

          
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
            {    
             if (_dbContext.Team.Any(a => a.ClientId == clientId))
                        ifClientExist = true;           
            }
           
            return ifClientExist;
        }
    }
}
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

        //Gather all data from the database about registered services
        public ICollection<ServiceDisplayResp> GetAllData(string clientId)
        {
            ICollection<ServiceDisplayResp> resultSet = null;
            if (!String.IsNullOrWhiteSpace(clientId))
            {
                using (_dbContext)
                {
                    if (_dbContext.Configuration.Where(a => a.OpenTo == clientId).Any())
                    {
                        
                            var endPointList = _dbContext.Configuration.SingleOrDefault(a => a.OpenTo == clientId).EndPoint;
                            var dataSet = _dbContext.Service.Join(_dbContext.Team, service => service.Owner,
                        team => team.ClientId, (service, team) => new
                        {
                            Username = team.Username,
                            Endpoint = service.Endpoint,
                            Input = service.Input,
                            Output = service.Output,
                            Dataformat = service.Dataformat,
                            Description = service.Description
                        }
                         ).Where(a => endPointList.Contains(a.Endpoint)).OrderBy(a => a.Username).ToList();

                            resultSet = new List<ServiceDisplayResp>();

                            foreach (var service in dataSet)
                            {
                                resultSet.Add(new ServiceDisplayResp(service.Endpoint, service.Username, service.Input, service.Output, service.Dataformat, service.Description));
                            }
                     
                      
                    }

                }
            }

            return resultSet;
        }

        //Method that will retrive result
        public bool IfClientExist(string clientId)
        {
            var ifClientExist = false;
            if (!String.IsNullOrWhiteSpace(clientId))
            {
                using (_dbContext)
                {
                    if (_dbContext.Team.Any(a => a.ClientId == clientId))
                        ifClientExist = true;
                }
            }
           
            return ifClientExist;
        }
    }
}
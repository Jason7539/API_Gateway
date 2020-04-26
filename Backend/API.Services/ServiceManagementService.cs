using API.Models.gateway;
using API.Models.json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.Services
{
    public class ServiceManagementService
    {

        private readonly ApiGatewayContext _context;
        public ServiceManagementService(ApiGatewayContext apiGatewayContext)
        {
            _context = apiGatewayContext;
        }

        public bool CreateService(CreateServicePost createServicePost)
        {
            try
            {
                // Create the service for that team.
                var newService = new Service()
                {
                    Owner = createServicePost.ClientId,
                    Endpoint = createServicePost.RouteToAccess,
                    Input = createServicePost.Input,
                    Output = createServicePost.Output,
                    Dataformat = createServicePost.DataFormat,
                    Description = createServicePost.ServiceDescription
                };
                // Save changes.
                _context.Service.Add(newService);
                _context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool CreateConfigurations(List<string> opentTo, string endPoint, string steps)
        {
            try
            {
                // Create configuration for each team.
                foreach (var team in opentTo)
                {
                    // Get the client id from username.
                    var clientId = _context.Team.
                                    Where(t => team == t.Username).
                                    Select(t => t.ClientId).
                                    ToList()[0];

                    var configuration = new Configuration() { OpenTo = clientId, EndPoint = endPoint, Steps = steps };

                    _context.Configuration.Add(configuration);
                
                }
                _context.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public List<string> GetTeamsUsername()
        {
            // Query to get all the team's username.
            var teams = _context.Team.
                        Select(team => team.Username).ToList();
            
            return teams;

        }

        public bool IsServiceEndpointUnique(string endpoint)
        {
            var service = _context.Service.
                            Where(s => endpoint == s.Endpoint)
                            .ToList();

            return service.Count == 0;
        }
    }
}

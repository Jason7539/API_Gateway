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
            // Create the service for that team.
            var newService = new Service() { Owner = createServicePost.ClientId, Endpoint = createServicePost.RouteToAccess, Input = createServicePost.Input,
                                            Output = createServicePost.Output, Dataformat = createServicePost.DataFormat, Description = createServicePost.ServiceDescription};



            // Loop the OpenTo array make a configuration for every team.

            _context.Service.Add(newService);
            _context.SaveChanges();

            return true;
        }

        public List<string> GetTeamsUsername()
        {
            // Query to get all the team's username.
            var teams = _context.Team.
                        Select(team => team.Username).ToList();
            
            return teams;

        }
    }
}

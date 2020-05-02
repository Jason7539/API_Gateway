using API.AppConstants;
using API.Models.gateway;
using API.Models.json;
using Microsoft.EntityFrameworkCore;
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

        /// <summary>
        /// Create a service for a team
        /// </summary>
        /// <param name="createServicePost">json request object containing information to create service</param>
        /// <returns>Bool representing whether the operation passed</returns>
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

        /// <summary>
        /// Create configuration of a service for open teams
        /// </summary>
        /// <param name="opentTo">List of strings containing teams to create configurations for</param>
        /// <param name="endPoint">End point for the configuration</param>
        /// <param name="steps">Steps to perform for the configuration</param>
        /// <returns>Bool representing whether the operation passed</returns>
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

        /// <summary>
        /// Get all the registered teams
        /// </summary>
        /// <returns>List of strings containing team's username</returns>
        public List<string> GetTeamsUsername()
        {
            // Query to get all the team's username.
            var teams = _context.Team.
                        Select(team => team.Username).ToList();
            
            return teams;

        }

        /// <summary>
        /// Test the uniqueness of a service's endpoint
        /// </summary>
        /// <param name="endpoint">Name of the endoint to test</param>
        /// <returns></returns>
        public bool IsServiceEndpointUnique(string endpoint)
        {
            var service = _context.Service.
                            Where(s => endpoint == s.Endpoint)
                            .ToList();

            return service.Count == 0;
        }

        /// <summary>
        /// Get the pagination length of services for a team
        /// </summary>
        /// <param name="clientId">Client id to get pagination length for</param>
        /// <returns>Int containing length of service pagination</returns>
        public int GetOwnedServicePagination(string clientId)
        {
            // Get pagination in multiples of Constants.ServiceManagementPagination.
            var ownedServiceNum = _context.Service.Where(s => clientId == s.Owner).ToList().Count;
            var pages = ownedServiceNum / Constants.ServiceManagementPagination;

            // Handle cases where pages is 0, a multiple of ServiceManagementPagination, and not a multiple.
            if(pages == 0)
            {
                return 1;
            }
            else if(ownedServiceNum % Constants.ServiceManagementPagination == 0)
            {
                return pages;
            }
            else
            {
                return pages + 1;
            }
        }

        /// <summary>
        /// Get the list of owned services for a team
        /// </summary>
        /// <param name="clientId">Client id to get owned services</param>
        /// <param name="pagination">pagination to specify service. Starts at 1</param>
        /// <returns>List of services for a team. Can be empty</returns>
        public List<ManageServiceResp> GetOwnedService(string clientId, int pagination)
        {
            return _context.Service.
                            Where(s => clientId == s.Owner).
                            OrderBy(s => s.Endpoint).
                            Select(s => new ManageServiceResp() { Endpoint = s.Endpoint, Input = s.Input, Output = s.Output, DataFormat = s.Dataformat, Description = s.Description }).
                            Skip((pagination - 1) * Constants.ServiceManagementPagination).Take(Constants.ServiceManagementPagination)
                            .ToList();

        }

        /// <summary>
        /// Delete a service and it's corresponding configuration
        /// </summary>
        /// <param name="endpoint">Service endpoint to delete</param>
        /// <returns>Bool representing whether the operation passed</returns>
        public bool DeleteService(string endpoint)
        {
            // Query the service
            var service = _context.Service.
                            Where(s => endpoint == s.Endpoint).
                            FirstOrDefault();

            // If we couldn't find service return false.
            if(service == null)
            {
                return false;
            }
            else
            {
                // Else delete the service if we found it
                _context.Service.Remove(service);
                _context.SaveChanges();
                return true;
            }
        }

        /// <summary>
        /// Update the configurations for a service. It does not delete the configurations for the owner.
        /// </summary>
        /// <param name="updateServicePatch">Json request object representing what to update</param>
        /// <returns>Bool representing whether the operation passed</returns>
        public bool UpdateServicePrivacy(UpdateServicePatch updateServicePatch)
        {
            // Save the configuration to re add it later.
            var saveConfig = _context.Configuration.AsNoTracking().
                            Where(c => updateServicePatch.EndPoint == c.EndPoint).
                            FirstOrDefault();
            
            // if we couldn't find the config return false
            if(saveConfig == null)
            {
                return false;
            }

            // Grab all Configuration for all the teams its open to except the owner. 
            var configs = _context.Configuration.
                            Where(c => updateServicePatch.EndPoint == c.EndPoint && updateServicePatch.ClientId != c.OpenTo).
                            Select(c => new Configuration() { EndPoint = c.EndPoint, OpenTo = c.OpenTo, Steps = c.Steps}).
                            ToList();

            // Delete them all. 
            _context.Configuration.RemoveRange(configs);

            // Loop through OpenTo and recreate config based on updated privacy rules.
            foreach(var team in updateServicePatch.OpenTo)
            {
                // Grab the client id based on username.
                var clientId = _context.Team.Where(t => team == t.Username).Select(t => t.ClientId).FirstOrDefault();

                // If clientId is owner skip.
                if(clientId == updateServicePatch.ClientId)
                {
                    continue;
                }

                // Skip the creation if username is not found.
                if(clientId == null)
                {
                    continue;
                }
                else
                {
                    // Recreate the configuration for the current team.
                    _context.Configuration.Add(new Configuration() {EndPoint = saveConfig.EndPoint, OpenTo = clientId, Steps = saveConfig.Steps });
                }
            }

            // Update the changes and return true.
            _context.SaveChanges();
            return true;
        }

        /// <summary>
        /// Get the teams that are allowed to use a service
        /// </summary>
        /// <param name="endpoint">Service endpoint to query</param>
        /// <returns>List of strings containing the teams username</returns>
        public List<string> GetAllowedConfigurationUsers(string endpoint)
        {
            // First get the the clientId of users.
            var clientId = _context.Configuration.
                            AsNoTracking().
                            Where(c => endpoint == c.EndPoint).
                            Select(c => c.OpenTo).
                            ToList();

            var usernameList = new List<string>();

            // Translate the client id to list of username.
            foreach(var id in clientId)
            {
                var username = _context.Team.
                                Where(t => id == t.ClientId).
                                Select(t => t.Username).
                                FirstOrDefault();
                // If we couldn't find the username skip it.
                if (username == null)
                {
                    continue;
                }

                usernameList.Add(username);

            }
            return usernameList;
        }

    }
}

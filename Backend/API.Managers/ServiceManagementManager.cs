using API.Models.json;
using API.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;


namespace API.Managers
{
    public class ServiceManagementManager
    {
        private readonly ServiceManagementService _serviceManagementService;
        private readonly UrlValidationService _urlValidationService;
        public ServiceManagementManager(ServiceManagementService serviceManagementService, UrlValidationService urlValidationService)
        {
            _serviceManagementService = serviceManagementService;
            _urlValidationService = urlValidationService;
        }

        /// <summary>
        /// Create a service for a team
        /// </summary>
        /// <param name="createServicePost">Json object representing request</param>
        /// <returns>Json response object</returns>
        public CreateServiceResp CreateService(CreateServicePost createServicePost)
        {
            // Check that endpoint to call the service is unique.
            var endpointResult = _serviceManagementService.IsServiceEndpointUnique(createServicePost.RouteToAccess);

            // Deserialize configurations into a .net object to loop over action chain for url validation.
            var configJson = JsonConvert.DeserializeObject<ServiceConfiguration>(createServicePost.Configurations);

            var websiteValid = true;

            // If endpoint is unique check if steps are alive.
            for (var i = 0; i < configJson.Steps; i++)
            {
                // Check if site is valid.
                websiteValid &= _urlValidationService.IsUrlValid(configJson.Configurations[i].Action);

                // Check if site is https.
                websiteValid &= _urlValidationService.IsUrlHttps(configJson.Configurations[i].Action);
            }

            var serviceCreateResult = false;
            var configurationsResult = false;

            // Create service configurations if above steps pass
            if(endpointResult && websiteValid)
            {
                // Create service for the team.
                serviceCreateResult = _serviceManagementService.CreateService(createServicePost);

                // Create Configurations for all teams the service is open to.
                configurationsResult = _serviceManagementService.CreateConfigurations(createServicePost.OpenTo, createServicePost.RouteToAccess, createServicePost.Configurations);
            }


            // Return corresponding json response object.
            if (endpointResult && websiteValid && serviceCreateResult && configurationsResult)
            {
                return new CreateServiceResp() { Status = true, EndpointResult = endpointResult, WebsiteValid = websiteValid };
            }
            else
            {
                return new CreateServiceResp() { Status = false, EndpointResult = endpointResult, WebsiteValid = websiteValid };
            }


        }

        /// <summary>
        /// Get the username of all registered team
        /// </summary>
        /// <returns>Json response object containing teams</returns>
        public GetTeamsResp GetTeamsUsername()
        {
            return new GetTeamsResp() { Teams = _serviceManagementService.GetTeamsUsername() };
        }

        /// <summary>
        /// Get the pagination length of services for a team
        /// </summary>
        /// <param name="clientId">Client id of the team to get the pagination for</param>
        /// <returns>Int length of the pagination</returns>
        public int GetOwnedServicePagination(string clientId)
        {
            return _serviceManagementService.GetOwnedServicePagination(clientId);
        }

        /// <summary>
        /// Get the list of owned services for a team
        /// </summary>
        /// <param name="clientId">Owner of the service</param>
        /// <param name="pagination">Page to fetch the service</param>
        /// <returns>List of json response object representing the services</returns>
        public List<ManageServiceResp> GetOwnedServices(string clientId, int pagination)
        {
            return _serviceManagementService.GetOwnedService(clientId, pagination);
        }

        /// <summary>
        /// Delete a service and its corresponding configuration
        /// </summary>
        /// <param name="endpoint">Service to delete</param>
        /// <returns>Bool representing whether the operation passed</returns>
        public bool DeleteService(string endpoint)
        {
            return _serviceManagementService.DeleteService(endpoint);
        }

        /// <summary>
        /// Update the privacy rules a service is open to
        /// </summary>
        /// <param name="updateServicePatch">Json request representing the changes</param>
        /// <returns>Bool representing whether the operation passed</returns>
        public bool UpdateServicePrivacy(UpdateServicePatch updateServicePatch)
        {
            return _serviceManagementService.UpdateServicePrivacy(updateServicePatch);
        }

        /// <summary>
        /// Get all the teams that a particular service is open to
        /// </summary>
        /// <param name="endpoint">Endpoint for the service to look</param>
        /// <returns>List of string containing the team's username</returns>
        public List<string> GetAllowedConfigurationUsers(string endpoint)
        {
            return _serviceManagementService.GetAllowedConfigurationUsers(endpoint);
        }

    }
}

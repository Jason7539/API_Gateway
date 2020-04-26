﻿using API.Models.json;
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

        public CreateServiceResp CreateService(CreateServicePost createServicePost)
        {
            // Check that endpoint to call the service is unique.
            var endpointResult = _serviceManagementService.IsServiceEndpointUnique(createServicePost.RouteToAccess);

            // Deserialize configurations into a .net object to loop over for url validation.
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


            if (endpointResult && websiteValid && serviceCreateResult && configurationsResult)
            {
                return new CreateServiceResp() { Status = true, EndpointResult = endpointResult, WebsiteValid = websiteValid };
            }
            else
            {
                return new CreateServiceResp() { Status = false, EndpointResult = endpointResult, WebsiteValid = websiteValid };
            }


        }

        public GetTeamsResp GetTeamsUsername()
        {
            return new GetTeamsResp() { Teams = _serviceManagementService.GetTeamsUsername() };
        }


    }
}

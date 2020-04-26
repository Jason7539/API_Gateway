using API.Models.json;
using API.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Managers
{
    public class ServiceManagementManager
    {
        private readonly ServiceManagementService _serviceManagementService;
        public ServiceManagementManager(ServiceManagementService serviceManagementService)
        {
            _serviceManagementService = serviceManagementService;
        }

        public bool CreateService(CreateServicePost createServicePost)
        {
            // Check that endpoint to call the service is unique.

            // Check that all steps to call this service are alive.




            return _serviceManagementService.CreateService(createServicePost);
        }

        public GetTeamsResp GetTeamsUsername()
        {
            return new GetTeamsResp() { Teams = _serviceManagementService.GetTeamsUsername() };
        }


    }
}

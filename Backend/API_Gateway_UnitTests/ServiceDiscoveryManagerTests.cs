using API.Managers;
using API.Models.gateway;
using API.Models.json;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace API_Gateway_UnitTests
{
    public class ServiceDiscoveryManagerTests
    {

        [TestMethod]
        public void GetAvailableServices_InMemory_Pass()
        {   //Arrange
            bool expected = true;
            bool actual = false;
            var option = new DbContextOptionsBuilder<ApiGatewayContext>()
                .UseInMemoryDatabase(databaseName: "GetAvailableServices_Pass_Database")
                .Options;
            ICollection<ServiceDisplayResp> registeredServices;

            //Act  
            using (var context = new ApiGatewayContext(option))
            {
                // Create a team first
                Team teamForTesting = new Team();
                string randomId = GenerateRandomKey(Int32.Parse(Environment.GetEnvironmentVariable("APIKeyInputLength", EnvironmentVariableTarget.User)));
                teamForTesting.ClientId = randomId;
                teamForTesting.WebsiteUrl = "testingWebSiteUrl";
                teamForTesting.Secret = "testingSecret";
                teamForTesting.CallbackUrl = "testingCallBackUrl";
                teamForTesting.Digest = "testingDigest";
                teamForTesting.Username = "testingUsername";
                context.Team.Add(teamForTesting);

                //Service 

                Service serviceForTesting = new Service();
                serviceForTesting.Endpoint = "testingEndPoint";
                serviceForTesting.Owner = randomId;//need to be the same as team's ClineId
                serviceForTesting.Id = 12345678;
                serviceForTesting.Input = "int";
                serviceForTesting.Output = "int";
                serviceForTesting.Dataformat = "xml";
                serviceForTesting.Description = "Some description for testing";
                context.Service.Add(serviceForTesting);

                //Configuration

                Configuration configForTesting = new Configuration();
                configForTesting.EndPoint = "testingEndPoint";//need to be the same as service
                configForTesting.OpenTo = randomId;
                configForTesting.Steps = "some steps for testing";
                context.Configuration.Add(configForTesting);
                context.SaveChanges();

                ServiceDiscoveryService serviceDiscoveryService = new ServiceDiscoveryService(context);
                ServiceDiscoveryManager serviceDiscoveryManager = new ServiceDiscoveryManager(serviceDiscoveryService);

                registeredServices = serviceDiscoveryManager.GetAvailableServices(randomId);
                if (registeredServices.Count > 0)
                    actual = true;
            }

            foreach (var service in registeredServices)
            {
                Trace.WriteLine(service.Endpoint + " " + service.Username + " " + service.Input + " " + service.Output + " " + service.Dataformat + " " + service.Description + Environment.NewLine);
            }

            //Assert
            Assert.AreEqual(expected, actual);

        }

        public string GenerateRandomKey(int length)
        {
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }

    }
}

using API.AppConstants;
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
    [TestClass]
    public class ServiceDiscoveryManagerTests
    {
        //Success condition for ServiceDiscoveryManager.GetAvailableServices(), if input is correct and it match the clientId, the method should return a list of services that open to the input clientId
        [TestMethod]
        public void GetAvailableServices_InMemory_Pass()
        {   //Arrange
            var expected = true;
            var actual = false;
            var option = new DbContextOptionsBuilder<ApiGatewayContext>()
                .UseInMemoryDatabase(databaseName: "ServiceDiscoveryManager.GetAvailableServices_Pass_Database")
                .Options;
            ICollection<ServiceDisplayResp> registeredServices;

            //Act  
            using (var context = new ApiGatewayContext(option))
            {
                // Create a team first
                var teamForTesting = new Team();
                var randomId = GenerateRandomKey(Int32.Parse(Environment.GetEnvironmentVariable("APIKeyInputLength", EnvironmentVariableTarget.User)));
                teamForTesting.ClientId = randomId;
                teamForTesting.WebsiteUrl = "testingWebSiteUrl";
                teamForTesting.Secret = "testingSecret";
                teamForTesting.CallbackUrl = "testingCallBackUrl";
                teamForTesting.Digest = "testingDigest";
                teamForTesting.Username = "testingUsername";
                context.Team.Add(teamForTesting);

                //Service 

                var serviceForTesting = new Service();
                serviceForTesting.Endpoint = "testingEndPoint";
                serviceForTesting.Owner = randomId;//need to be the same as team's ClineId
                serviceForTesting.Id = 12345678;
                serviceForTesting.Input = "int";
                serviceForTesting.Output = "int";
                serviceForTesting.Dataformat = "xml";
                serviceForTesting.Description = "Some description for testing";
                context.Service.Add(serviceForTesting);

                //Configuration

                var configForTesting = new Configuration();
                configForTesting.EndPoint = "testingEndPoint";//need to be the same as service
                configForTesting.OpenTo = randomId;
                configForTesting.Steps = "some steps for testing";
                context.Configuration.Add(configForTesting);
                context.SaveChanges();

                var serviceDiscoveryService = new ServiceDiscoveryService(context);
                var serviceDiscoveryManager = new ServiceDiscoveryManager(serviceDiscoveryService);

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


        //Fail condition for ServiceDiscoveryManager.GetAvailableServices(), when input is null, no data will be retrieved
        [TestMethod]
        public void GetServices_NotPass_NullInput()
        {   //Arrange
            var expected = false;
            var actual = false;
            ICollection<ServiceDisplayResp> registeredServices;

            //Act
            using (var context = new ApiGatewayContext())
            {
                var serviceDiscoveryService = new ServiceDiscoveryService(context);
                var serviceDiscoveryManager = new ServiceDiscoveryManager(serviceDiscoveryService);
                registeredServices = serviceDiscoveryManager.GetAvailableServices(null);
            }



            //Since input is invalid, the registeredServices should be null
            if (registeredServices != null)
                actual = true;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        //Fail condition for ServiceDiscoveryManager.GetAvailableServices(), when input is white space, no data will be retrieved
        [TestMethod]
        public void GetServices_NotPass_WhiteSpaceInput()
        {   //Arrange
            var expected = false;
            var actual = false;
            ICollection<ServiceDisplayResp> registeredServices;

            //Act
            using (var context = new ApiGatewayContext())
            {
                var serviceDiscoveryService = new ServiceDiscoveryService(context);
                var serviceDiscoveryManager = new ServiceDiscoveryManager(serviceDiscoveryService);
                registeredServices = serviceDiscoveryManager.GetAvailableServices("");
            }

            //Since input is invalid, the registeredServices should be null
            if (registeredServices != null)
                actual = true;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        //Fail condition for ServiceDiscoveryManager.GetAvailableServices(), when input is too long, return no data
        public void GetServices_NotPass_InputTooLong()
        {   //Arrange
            var expected = false;
            var actual = false;
            ICollection<ServiceDisplayResp> registeredServices;

            //Act
            using (var context = new ApiGatewayContext())
            {
                var serviceDiscoveryService = new ServiceDiscoveryService(context);
                var serviceDiscoveryManager = new ServiceDiscoveryManager(serviceDiscoveryService);
                //Generate a key that is longer than requirement
                var randomId = GenerateRandomKey(Constants.clientIdLength + 1);
                registeredServices = serviceDiscoveryManager.GetAvailableServices(randomId);
            }

            //Since input is invalid, the registeredServices should be null
            if (registeredServices != null)
                actual = true;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        //Fail condition for ServiceDiscoveryManager.GetAvailableServices(), when input is too short, return no data
        public void GetServices_NotPass_InputTooShort()
        {   //Arrange
            var expected = false;
            var actual = false;
            ICollection<ServiceDisplayResp> registeredServices;

            //Act
            using (var context = new ApiGatewayContext())
            {
                var serviceDiscoveryService = new ServiceDiscoveryService(context);
                var serviceDiscoveryManager = new ServiceDiscoveryManager(serviceDiscoveryService);
                //Generate a key that is shorter than requirement
                var randomId = GenerateRandomKey(Constants.clientIdLength - 1);
                registeredServices = serviceDiscoveryManager.GetAvailableServices(randomId);
            }

            //Since input is invalid, the registeredServices should be null
            if (registeredServices != null)
                actual = true;

            //Assert
            Assert.AreEqual(expected, actual);
        }


        //Fail condition for ServiceDiscoveryManager.GetAvailableServices(), when no value is in the database, no data will be retrieved
        [TestMethod]
        public void GetServices_NotPass_EmptyDatabase()
        {   //Arrange
            var expected = false;
            var actual = false;
            ICollection<ServiceDisplayResp> registeredServices;
            var option = new DbContextOptionsBuilder<ApiGatewayContext>()
                .UseInMemoryDatabase(databaseName: "ServiceDiscoveryManager.GetServices_Empty_database")
                .Options;
            //Act
            using (var context = new ApiGatewayContext(option))
            {
                var serviceDiscoveryService = new ServiceDiscoveryService(context);
                var serviceDiscoveryManager = new ServiceDiscoveryManager(serviceDiscoveryService);
                registeredServices = serviceDiscoveryManager.GetAvailableServices(GenerateRandomKey(Int32.Parse(Environment.GetEnvironmentVariable("APIKeyInputLength", EnvironmentVariableTarget.User))));
            }

            //Since input is invalid, the registeredServices should be null
            if (registeredServices != null)
                actual = true;

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

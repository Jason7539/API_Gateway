﻿using API.DAL;
using API.Models.gateway;
using API.Models.json;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace API_Gateway_UnitTests
{
    [TestClass]
    public class ServiceDiscoveryServiceTests
    {
        //Success condition for ServiceDiscoveryService.GetServices(), if input is correct and it match the clientId, the method should return a list of services that open to the input clientId
        [TestMethod]
        public void GetServices_InMemory_Pass()
        {   //Arrange
            var expected = true;
            var actual = false;
            var option = new DbContextOptionsBuilder<ApiGatewayContext>()
                .UseInMemoryDatabase(databaseName: "ServiceDiscoveryService.GetServices_Pass_Database")
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

                var serviceDiscoverService = new ServiceDiscoveryService(context);

                registeredServices = serviceDiscoverService.GetServices(randomId);
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

        //Fail condition for ServiceDiscoveryService.GetServices(), when input is null, no data will be retrieved
        [TestMethod]
        public void GetServices_NotPass_NullInput()
        {   //Arrange
            var expected = false;
            var actual = false;
            ICollection<ServiceDisplayResp> registeredServices;

            //Act
            using (var context = new ApiGatewayContext())
            {
                var serviceDiscoverService = new ServiceDiscoveryService(context);
                registeredServices = serviceDiscoverService.GetServices(null);
            }



            //Since input is invalid, the registeredServices should be null
            if (registeredServices != null)
                actual = true;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        //Fail condition for ServiceDiscoveryService.GetServices(), when input is white space, no data will be retrieved
        [TestMethod]
        public void GetServices_NotPass_WhiteSpaceInput()
        {   //Arrange
            var expected = false;
            var actual = false;
            ICollection<ServiceDisplayResp> registeredServices;

            //Act
            using (var context = new ApiGatewayContext())
            {
                var serviceDiscoverService = new ServiceDiscoveryService(context);
                registeredServices = serviceDiscoverService.GetServices("");
            }

            //Since input is invalid, the registeredServices should be null
            if (registeredServices != null)
                actual = true;

            //Assert
            Assert.AreEqual(expected, actual);
        }


        //Fail condition for ServiceDiscoveryService.GetServices(), when no value is in the database, no data will be retrieved
        [TestMethod]
        public void GetServices_NotPass_EmptyDatabase()
        {   //Arrange
            var expected = false;
            var actual = false;
            ICollection<ServiceDisplayResp> registeredServices;
            var option = new DbContextOptionsBuilder<ApiGatewayContext>()
                .UseInMemoryDatabase(databaseName: "ServiceDiscoveryService.GetServices_Empty_database")
                .Options;
            //Act
            using (var context = new ApiGatewayContext(option))
            {
                var serviceDiscoverService = new ServiceDiscoveryService(context);
                registeredServices = serviceDiscoverService.GetServices(GenerateRandomKey(Int32.Parse(Environment.GetEnvironmentVariable("APIKeyInputLength", EnvironmentVariableTarget.User))));
            }

            //Since input is invalid, the registeredServices should be null
            if (registeredServices != null)
                actual = true;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        //Success condition for ServiceDiscoveryService.IfClientExist(), if input is correct and it match the clientId in database then it return a true
        [TestMethod]
        public void IfClientExist_InMemory_Pass()
        {   //Arrange
            var expected = true;
            var actual = false;
            var option = new DbContextOptionsBuilder<ApiGatewayContext>()
                .UseInMemoryDatabase(databaseName: "ServiceDiscoveryService.IfClientExist_Pass_database")
                .Options;

            //Act  
            using (var context = new ApiGatewayContext(option))
            {
                var teamForTesting = new Team();
                var randomKey = GenerateRandomKey(Int32.Parse(Environment.GetEnvironmentVariable("APIKeyInputLength", EnvironmentVariableTarget.User)));
                teamForTesting.ClientId = randomKey;
                teamForTesting.WebsiteUrl = "testingWebSiteUrl";
                teamForTesting.Secret = "testingSecret";
                teamForTesting.CallbackUrl = "testingCallBackUrl";
                teamForTesting.Digest = "testingDigest";
                teamForTesting.Username = "testingUsername";
                context.Team.Add(teamForTesting);
                context.SaveChanges();

                var serviceDiscoverService = new ServiceDiscoveryService(context);

                //Different from GetServices, check if a team exist need the same random key
                actual = serviceDiscoverService.IfClientExist(randomKey);
            }

            //Assert
            Assert.AreEqual(expected, actual);
        }

        //Fail condition for ServiceDiscoveryService.IfClientExist(), if the input clientId does not match any 
        [TestMethod]
        public void IfClientExist_InMemory_ClientDoesnotMatch()
        {   //Arrange
            var expected = false;
            var actual = false;
            var option = new DbContextOptionsBuilder<ApiGatewayContext>()
                .UseInMemoryDatabase(databaseName: "ServiceDiscoveryService.IfClientExist_NotPass_database")
                .Options;

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

                var serviceDiscoverService = new ServiceDiscoveryService(context);
                var inputClientId = GenerateRandomKey(Int32.Parse(Environment.GetEnvironmentVariable("APIKeyInputLength", EnvironmentVariableTarget.User)));

                //Make sure the second random generated clientId is different from the first one
                while (inputClientId == randomId)
                {
                    inputClientId = GenerateRandomKey(Int32.Parse(Environment.GetEnvironmentVariable("APIKeyInputLength", EnvironmentVariableTarget.User)));
                }

                actual = serviceDiscoverService.IfClientExist(inputClientId);
            }

            //Assert
            Assert.AreEqual(expected, actual);
        }

        //Fail condition for ServiceDiscoveryService.IfClientExist(), if the database is empty, it should return a false.
        [TestMethod]
        public void IfClientExist_InMemory_EmptyDatabase()
        {   //Arrange
            var expected = false;
            var actual = false;
            var option = new DbContextOptionsBuilder<ApiGatewayContext>()
                .UseInMemoryDatabase(databaseName: "ServiceDiscoveryService.IfClientExist_NotPass_database")
                .Options;

            //Act  
            using (var context = new ApiGatewayContext(option))
            {
                var serviceDiscoverService = new ServiceDiscoveryService(context);
                var inputClientId = GenerateRandomKey(Int32.Parse(Environment.GetEnvironmentVariable("APIKeyInputLength", EnvironmentVariableTarget.User)));

                actual = serviceDiscoverService.IfClientExist(inputClientId);
            }

            //Assert
            Assert.AreEqual(expected, actual);
        }

        //Fail condition for ServiceDiscoveryService.IfClientExist(), if the input is null, then it return a false
        [TestMethod]
        public void IfClientExist_NullInput()
        {   //Arrange
            var expected = false;
            var actual = false;

            //Act  
            using (var context = new ApiGatewayContext())
            {
                var serviceDiscoverService = new ServiceDiscoveryService(context);
                actual = serviceDiscoverService.IfClientExist(null);
            }

            //Assert
            Assert.AreEqual(expected, actual);
        }

        //Fail condition for ServiceDiscoveryService.IfClientExist(), if the input is white space then it  return a false
        [TestMethod]
        public void IfClientExist_WhiteSpaceInput()
        {   //Arrange
            var expected = false;
            var actual = false;

            //Act  
            using (var context = new ApiGatewayContext())
            {
                var serviceDiscoverService = new ServiceDiscoveryService(context);
                actual = serviceDiscoverService.IfClientExist("");
            }

            //Assert
            Assert.AreEqual(expected, actual);
        }


        //Tool that can random generate a clientId
        public string GenerateRandomKey(int length)
        {
            var random = new Random();
            var s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }

    }
}

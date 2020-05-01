using System;
using System.Collections.Generic;
using System.Diagnostics;
using API.DAL;
using API.Models.gateway;
using API.Models.json;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace API_Gateway_UnitTests
{
    [TestClass]
    public class SqlServiceDisplayDAOTest
    {

        [TestMethod]
        public void GetAllData_InMemory_Pass()
        {   //Arrange
            bool expected = true;
            bool actual = false;
            var option = new DbContextOptionsBuilder<ApiGatewayContext>()
                .UseInMemoryDatabase(databaseName: "GetAllData_Pass_Database")
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

                ServiceDiscoveryDAO sqlServiceDisplayDAO = new ServiceDiscoveryDAO(context);
        
                registeredServices = sqlServiceDisplayDAO.GetAllData(randomId);
                if (registeredServices.Count > 0)
                    actual = true;
            }

            foreach (var service in registeredServices)
            {
                Trace.WriteLine(service.Endpoint +" "+ service.Username+" "+service.Input+" "+service.Output+" "+service.Dataformat+" "+service.Description+Environment.NewLine);
            }

            //Assert
            Assert.AreEqual(expected, actual);

        }



        //Fail condition for GetAllData, when input is null, no data will be retrieved
        [TestMethod]
        public void GetAllData_NotPass_NullInput()
        {   //Arrange
            bool expected = false;
            bool actual = false;
            ICollection<ServiceDisplayResp> registeredServices;

            //Act
            using (var context = new ApiGatewayContext())
            {
                ServiceDiscoveryDAO sqlServiceDisplayDAO = new ServiceDiscoveryDAO(context);
                registeredServices = sqlServiceDisplayDAO.GetAllData(null);
            }



            //Since input is invalid, the registeredServices should be null
            if (registeredServices != null)
                actual = true;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        //Fail condition for GetAllData, when input is white space, no data will be retrieved
        [TestMethod]
        public void GetAllData_NotPass_WhiteSpaceInput()
        {   //Arrange
            bool expected = false;
            bool actual = false;
            ICollection<ServiceDisplayResp> registeredServices;

            //Act
            using (var context = new ApiGatewayContext())
            {
                ServiceDiscoveryDAO sqlServiceDisplayDAO = new ServiceDiscoveryDAO(context);
                registeredServices = sqlServiceDisplayDAO.GetAllData("");
            }

            //Since input is invalid, the registeredServices should be null
            if (registeredServices != null)
                actual = true;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        //Fail condition for GetAllData, when input is out of range, no data will be retrieved
        [TestMethod]
        public void GetAllData_NotPass_InputIsTooLong()
        {   //Arrange
            bool expected = false;
            bool actual = false;
            ICollection<ServiceDisplayResp> registeredServices;

            //Act
            using (var context = new ApiGatewayContext())
            {
                ServiceDiscoveryDAO sqlServiceDisplayDAO = new ServiceDiscoveryDAO(context);
                //Generate a random key that is longer than the requirement
                string randomTestKey = GenerateRandomKey(Int32.Parse(Environment.GetEnvironmentVariable("APIKeyInputLength", EnvironmentVariableTarget.User)) + 1);
                registeredServices = sqlServiceDisplayDAO.GetAllData(randomTestKey);
            }

            //Since input is invalid, the registeredServices should be null
            if (registeredServices != null)
                actual = true;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        //Fail condition for GetAllData, when input does not have 32 characters, no data will be retrieved
        [TestMethod]
        public void GetAllData_NotPass_InputIsTooShort()
        {   //Arrange
            bool expected = false;
            bool actual = false;
            ICollection<ServiceDisplayResp> registeredServices;

            //Act
            using (var context = new ApiGatewayContext())
            {
                ServiceDiscoveryDAO sqlServiceDisplayDAO = new ServiceDiscoveryDAO(context);
                //Generate a random key that is longer than the requirement
                string randomTestKey = GenerateRandomKey(Int32.Parse(Environment.GetEnvironmentVariable("APIKeyInputLength", EnvironmentVariableTarget.User)) - 1);
                registeredServices = sqlServiceDisplayDAO.GetAllData(randomTestKey);
            }

            //Since input is invalid, the registeredServices should be null
            if (registeredServices != null)
                actual = true;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        //Fail condition for GetAllData, when no value is not in the database, no data will be retrieved
        [TestMethod]
        public void GetAllData_NotPass_NoResult()
        {   //Arrange
            bool expected = false;
            bool actual = false;
            ICollection<ServiceDisplayResp> registeredServices;
            var option = new DbContextOptionsBuilder<ApiGatewayContext>()
                .UseInMemoryDatabase(databaseName: "GetAllData_Empty_database")
                .Options;
            //Act
            using (var context = new ApiGatewayContext(option))
            {
                ServiceDiscoveryDAO sqlServiceDisplayDAO = new ServiceDiscoveryDAO(context);
                registeredServices = sqlServiceDisplayDAO.GetAllData(GenerateRandomKey(Int32.Parse(Environment.GetEnvironmentVariable("APIKeyInputLength", EnvironmentVariableTarget.User))));
            }

            //Since input is invalid, the registeredServices should be null
            if (registeredServices!=null)
                actual = true;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IfTeamExist_InMemory_Pass()
        {   //Arrange
            bool expected = true;
            bool actual = false;
            var option = new DbContextOptionsBuilder<ApiGatewayContext>()
                .UseInMemoryDatabase(databaseName: "IfTeamExist_Pass_database")
                .Options;

            //Act  
            using (var context = new ApiGatewayContext(option))
            {
                Team teamForTesting = new Team();
                string randomKey = GenerateRandomKey(Int32.Parse(Environment.GetEnvironmentVariable("APIKeyInputLength", EnvironmentVariableTarget.User)));
                teamForTesting.ClientId = randomKey;
                teamForTesting.WebsiteUrl = "testingWebSiteUrl";
                teamForTesting.Secret = "testingSecret";
                teamForTesting.CallbackUrl = "testingCallBackUrl";
                teamForTesting.Digest = "testingDigest";
                teamForTesting.Username = "testingUsername";
                context.Team.Add(teamForTesting);
                context.SaveChanges();

                ServiceDiscoveryDAO sqlServiceDisplayDAO = new ServiceDiscoveryDAO(context);

                //Different from GetAllData, check if a team exist need the same random key
                actual = sqlServiceDisplayDAO.IfClientExist(randomKey);
            }

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IfTeamExist_InMemory_TeamDoesnotExist()
        {   //Arrange
            bool expected = false;
            bool actual = false;
            var option = new DbContextOptionsBuilder<ApiGatewayContext>()
                .UseInMemoryDatabase(databaseName: "IfTeamExist_NotPass_database")
                .Options;

            //Act  
            using (var context = new ApiGatewayContext(option))
            {
                ServiceDiscoveryDAO sqlServiceDisplayDAO = new ServiceDiscoveryDAO(context);
                actual = sqlServiceDisplayDAO.IfClientExist(GenerateRandomKey(Int32.Parse(Environment.GetEnvironmentVariable("APIKeyInputLength", EnvironmentVariableTarget.User))));
            }

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IfTeamExist_NullInput()
        {   //Arrange
            bool expected = false;
            bool actual = false;

            //Act  
            using (var context = new ApiGatewayContext())
            {
                ServiceDiscoveryDAO sqlServiceDisplayDAO = new ServiceDiscoveryDAO(context);
                actual = sqlServiceDisplayDAO.IfClientExist(null);
            }

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IfTeamExist_WhiteSpaceInput()
        {   //Arrange
            bool expected = false;
            bool actual = false;

            //Act  
            using (var context = new ApiGatewayContext())
            {
                ServiceDiscoveryDAO sqlServiceDisplayDAO = new ServiceDiscoveryDAO(context);
                actual = sqlServiceDisplayDAO.IfClientExist("");
            }

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IfTeamExist_InputTooLong()
        {   //Arrange
            bool expected = false;
            bool actual = false;

            //Act  
            using (var context = new ApiGatewayContext())
            {
                ServiceDiscoveryDAO sqlServiceDisplayDAO = new ServiceDiscoveryDAO(context);
                actual = sqlServiceDisplayDAO.IfClientExist(GenerateRandomKey(Int32.Parse(Environment.GetEnvironmentVariable("APIKeyInputLength", EnvironmentVariableTarget.User)) + 1));
            }

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IfTeamExist_InputTooShort()
        {   //Arrange
            bool expected = false;
            bool actual = false;

            //Act  
            using (var context = new ApiGatewayContext())
            {
                ServiceDiscoveryDAO sqlServiceDisplayDAO = new ServiceDiscoveryDAO(context);
                actual = sqlServiceDisplayDAO.IfClientExist(GenerateRandomKey(Int32.Parse(Environment.GetEnvironmentVariable("APIKeyInputLength", EnvironmentVariableTarget.User)) - 1));
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

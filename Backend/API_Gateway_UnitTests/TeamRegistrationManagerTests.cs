using API.Managers;
using API.Models.gateway;
using API.Models.json;
using API.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace API_Gateway_UnitTests
{
    [TestClass]
    public class TeamRegistrationManagerTests
    {

        private static IEnumerable<object[]> ValidTeamCreation =>
        new List<object[]> {
                    new object[] {new TeamRegisterPost() {Username = "USERNAME", Password = "VALIDPasswordsdf", RepeatPassword = "VALIDPasswordsdf", CallbackUrl ="https://www.google.com/search?q=google+dictionary&oq=google+diction&aqs=chrome.0.69i59j69i57j69i64.2061j0j1&sourceid=chrome&ie=UTF-8", WebsiteUrl="https://www.google.com/search?q=google+dictionary&oq=google+diction&aqs=chrome.0.69i59j69i57j69i64.2061j0j1&sourceid=chrome&ie=UTF-8" } },
        };


        [TestMethod]
        [DynamicData(nameof(ValidTeamCreation))]
        public void CreateValidTeamPass(TeamRegisterPost request)
        {
            // Arrange DI of objects
            var _context = new ApiGatewayContext();
            var teamRegistrationService = new TeamRegistrationService(_context);
            var urlValidationService = new UrlValidationService(_context);
            
            var teamRegistrationManager = new TeamRegistrationManager(teamRegistrationService, urlValidationService);

            // Act create a valid team
            var creatTeamStatus = teamRegistrationManager.CreateTeamAccount(request);

            // Assert that team creation is successfull
            Assert.IsTrue(creatTeamStatus.TeamCreate);

            // Cleanup the team

            var createdTeam = _context.Team.
                             Where(t => request.Username == t.Username).
                             FirstOrDefault();
            if(createdTeam == null)
            {
                // Failed to delete
                Assert.IsTrue(false);
            }
            _context.Team.Remove(createdTeam);
            _context.SaveChanges();
        }

        // Create Invalid Team 
        private static IEnumerable<object[]> InvalidTeamSmallUsername =>
        new List<object[]> {
                    new object[] {new TeamRegisterPost() {Username = "as", Password = "asadasdasdasds", RepeatPassword = "VALIDPasswordsdf", CallbackUrl ="https://www.google.com/search?q=google+dictionary&oq=google+diction&aqs=chrome.0.69i59j69i57j69i64.2061j0j1&sourceid=chrome&ie=UTF-8", WebsiteUrl="https://www.google.com/search?q=google+dictionary&oq=google+diction&aqs=chrome.0.69i59j69i57j69i64.2061j0j1&sourceid=chrome&ie=UTF-8" } },
        };


        [TestMethod]
        [DynamicData(nameof(InvalidTeamSmallUsername))]
        public void CreateInvalidTeamSmallUsernameFail(TeamRegisterPost request)
        {
            // Arrange DI of objects
            var _context = new ApiGatewayContext();
            var teamRegistrationService = new TeamRegistrationService(_context);
            var urlValidationService = new UrlValidationService(_context);

            var teamRegistrationManager = new TeamRegistrationManager(teamRegistrationService, urlValidationService);

            // Act create a valid team
            var creatTeamStatus = teamRegistrationManager.CreateTeamAccount(request);

            // Assert that team creation is successfull
            Assert.IsFalse(creatTeamStatus.TeamCreate);
        }
        // Callback Url not alive
        private static IEnumerable<object[]> InvalidTeamCallbackUrlNotAlive =>
            new List<object[]> {
                    new object[] {new TeamRegisterPost() {Username = "asasdasdasd", Password = "asasdasdasdasdasdasdasdasd", RepeatPassword = "VALIDPasswordsdf", CallbackUrl ="a", WebsiteUrl="https://www.google.com/search?q=google+dictionary&oq=google+diction&aqs=chrome.0.69i59j69i57j69i64.2061j0j1&sourceid=chrome&ie=UTF-8" } },
        };


        [TestMethod]
        [DynamicData(nameof(InvalidTeamCallbackUrlNotAlive))]
        public void CreateInvalidTeamInvalidUrlFail(TeamRegisterPost request)
        {
            // Arrange DI of objects
            var _context = new ApiGatewayContext();
            var teamRegistrationService = new TeamRegistrationService(_context);
            var urlValidationService = new UrlValidationService(_context);

            var teamRegistrationManager = new TeamRegistrationManager(teamRegistrationService, urlValidationService);

            // Act create a valid team
            var creatTeamStatus = teamRegistrationManager.CreateTeamAccount(request);

            // Assert that team creation is successfull
            Assert.IsFalse(creatTeamStatus.TeamCreate);
        }



        // website Url not alive
        private static IEnumerable<object[]> InvalidWebsiteUrl =>
            new List<object[]> {
                    new object[] {new TeamRegisterPost() {Username = "aasdasds", Password = "assdfsdfsdfsdfsdfsasddfsdf", RepeatPassword = "VALIDPasswordsdf", CallbackUrl = "https://www.google.com/", WebsiteUrl="a" } },
        };


        [TestMethod]
        [DynamicData(nameof(InvalidWebsiteUrl))]
        public void CreateInvalidTeamWebsiteInvalidFail(TeamRegisterPost request)
        {
            // Arrange DI of objects
            var _context = new ApiGatewayContext();
            var teamRegistrationService = new TeamRegistrationService(_context);
            var urlValidationService = new UrlValidationService(_context);

            var teamRegistrationManager = new TeamRegistrationManager(teamRegistrationService, urlValidationService);

            // Act create a valid team
            var creatTeamStatus = teamRegistrationManager.CreateTeamAccount(request);

            // Assert that team creation is successfull
            Assert.IsFalse(creatTeamStatus.TeamCreate);
        }

    }
}

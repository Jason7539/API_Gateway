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
    public class TeamLoginManagerTests
    {
        private static IEnumerable<object[]> ValidTeamLogin =>
        new List<object[]> {
            new object[] {new TeamRegisterPost() {Username = "USERNAME", Password = "VALIDPasswordsdf", RepeatPassword = "VALIDPasswordsdf", CallbackUrl ="https://www.google.com/search?q=google+dictionary&oq=google+diction&aqs=chrome.0.69i59j69i57j69i64.2061j0j1&sourceid=chrome&ie=UTF-8", WebsiteUrl="https://www.google.com/search?q=google+dictionary&oq=google+diction&aqs=chrome.0.69i59j69i57j69i64.2061j0j1&sourceid=chrome&ie=UTF-8" }, new TeamLoginPost() {Username= "USERNAME", Password= "VALIDPasswordsdf" } },
        };

        [TestMethod]
        [DynamicData(nameof(ValidTeamLogin))]
        public void ValidTeamLoginPass(TeamRegisterPost teamRegisterPost, TeamLoginPost teamLoginPost)
        {
            // Arrange DI of objects
            var _context = new ApiGatewayContext();
            var teamRegistrationService = new TeamRegistrationService(_context);
            var urlValidationService = new UrlValidationService(_context);

            var teamRegistrationManager = new TeamRegistrationManager(teamRegistrationService, urlValidationService);

            var creatTeamStatus = teamRegistrationManager.CreateTeamAccount(teamRegisterPost);

            // Assert that team creation is successfull
            Assert.IsTrue(creatTeamStatus.TeamCreate);

            // DI of team login
            var teamLoginService = new TeamLoginService(_context);
            var jwtService = new JWTService();

            var teamLoginManager = new TeamLoginManager(teamLoginService, jwtService);

            // Act login for the registered user.
            var loginresp = teamLoginManager.TeamLogin(teamLoginPost);

            // Assert that login passed
            Assert.IsTrue(loginresp.Status);

            // Cleanup the team
            var createdTeam = _context.Team.
                             Where(t => teamRegisterPost.Username == t.Username).
                             FirstOrDefault();
            if (createdTeam == null)
            {
                // Failed to delete
                Assert.IsTrue(false);
            }
            _context.Team.Remove(createdTeam);
            _context.SaveChanges();
        }

        // Invalid username login
        private static IEnumerable<object[]> InvalidValidTeamLoginWrongUsername =>
 new List<object[]> {
            new object[] {new TeamRegisterPost() {Username = "USERNAME", Password = "VALIDPasswordsdf", RepeatPassword = "VALIDPasswordsdf", CallbackUrl ="https://www.google.com/search?q=google+dictionary&oq=google+diction&aqs=chrome.0.69i59j69i57j69i64.2061j0j1&sourceid=chrome&ie=UTF-8", WebsiteUrl="https://www.google.com/search?q=google+dictionary&oq=google+diction&aqs=chrome.0.69i59j69i57j69i64.2061j0j1&sourceid=chrome&ie=UTF-8" }, new TeamLoginPost() {Username= "asssdfsdf", Password= "VALIDPasswordsdf" } },
 };

        [TestMethod]
        [DynamicData(nameof(InvalidValidTeamLoginWrongUsername))]
        public void InvalidTeamLoginWrongUsernameFail(TeamRegisterPost teamRegisterPost, TeamLoginPost teamLoginPost)
        {
            // Arrange DI of objects
            var _context = new ApiGatewayContext();
            var teamRegistrationService = new TeamRegistrationService(_context);
            var urlValidationService = new UrlValidationService(_context);

            var teamRegistrationManager = new TeamRegistrationManager(teamRegistrationService, urlValidationService);

            var creatTeamStatus = teamRegistrationManager.CreateTeamAccount(teamRegisterPost);

            // Assert that team creation is successfull
            Assert.IsTrue(creatTeamStatus.TeamCreate);

            // DI of team login
            var teamLoginService = new TeamLoginService(_context);
            var jwtService = new JWTService();

            var teamLoginManager = new TeamLoginManager(teamLoginService, jwtService);

            // Act login for the registered user.
            var loginresp = teamLoginManager.TeamLogin(teamLoginPost);

            // Assert that login fail
            Assert.IsFalse(loginresp.Status);

            // Cleanup the team
            var createdTeam = _context.Team.
                             Where(t => teamRegisterPost.Username == t.Username).
                             FirstOrDefault();
            if (createdTeam == null)
            {
                // Failed to delete
                Assert.IsTrue(false);
            }
            _context.Team.Remove(createdTeam);
            _context.SaveChanges();
        }


        // Invalid password login
        private static IEnumerable<object[]> InvalidValidTeamLoginWrongPassword =>
         new List<object[]> {
                    new object[] {new TeamRegisterPost() {Username = "USERNAME", Password = "VALIDPasswordsdf", RepeatPassword = "VALIDPasswordsdf", CallbackUrl ="https://www.google.com/search?q=google+dictionary&oq=google+diction&aqs=chrome.0.69i59j69i57j69i64.2061j0j1&sourceid=chrome&ie=UTF-8", WebsiteUrl="https://www.google.com/search?q=google+dictionary&oq=google+diction&aqs=chrome.0.69i59j69i57j69i64.2061j0j1&sourceid=chrome&ie=UTF-8" }, new TeamLoginPost() {Username= "USERNAME", Password= "VALIDPasswor2222222" } },
         };

        [TestMethod]
        [DynamicData(nameof(InvalidValidTeamLoginWrongPassword))]
        public void InvalidTeamLoginWrongPasswordFail(TeamRegisterPost teamRegisterPost, TeamLoginPost teamLoginPost)
        {
            // Arrange DI of objects
            var _context = new ApiGatewayContext();
            var teamRegistrationService = new TeamRegistrationService(_context);
            var urlValidationService = new UrlValidationService(_context);

            var teamRegistrationManager = new TeamRegistrationManager(teamRegistrationService, urlValidationService);

            var creatTeamStatus = teamRegistrationManager.CreateTeamAccount(teamRegisterPost);

            // Assert that team creation is successfull
            Assert.IsTrue(creatTeamStatus.TeamCreate);

            // DI of team login
            var teamLoginService = new TeamLoginService(_context);
            var jwtService = new JWTService();

            var teamLoginManager = new TeamLoginManager(teamLoginService, jwtService);

            // Act login for the registered user.
            var loginresp = teamLoginManager.TeamLogin(teamLoginPost);

            // Assert that login fail
            Assert.IsFalse(loginresp.Status);

            // Cleanup the team
            var createdTeam = _context.Team.
                             Where(t => teamRegisterPost.Username == t.Username).
                             FirstOrDefault();
            if (createdTeam == null)
            {
                // Failed to delete
                Assert.IsTrue(false);
            }
            _context.Team.Remove(createdTeam);
            _context.SaveChanges();
        }

        // Login with nonexistent account
        private static IEnumerable<object[]> InvalidNonExistentAccount =>
        new List<object[]> {
                 new object[] { new TeamLoginPost() {Username= "USERNAME", Password= "VALIDPasswor2222222" } },
        };

        [TestMethod]
        [DynamicData(nameof(InvalidNonExistentAccount))]
        public void InvalidNonExistentAccountLoginFail( TeamLoginPost teamLoginPost)
        {

            // DI of team login
            var _context = new ApiGatewayContext();
            var teamLoginService = new TeamLoginService(_context);
            var jwtService = new JWTService();

            var teamLoginManager = new TeamLoginManager(teamLoginService, jwtService);

            // Act login for the registered user.
            var loginresp = teamLoginManager.TeamLogin(teamLoginPost);

            // Assert that login fail
            Assert.IsFalse(loginresp.Status);
        }
    }
}

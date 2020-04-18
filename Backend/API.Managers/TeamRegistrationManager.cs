using API.AppConstants;
using API.Models.gateway;
using API.Models.json;
using API.Services;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;

namespace API.Managers
{
    public class TeamRegistrationManager
    {
        private readonly TeamRegistrationService _teamRegistrationService;
        public TeamRegistrationManager(TeamRegistrationService teamRegistrationService)
        {
            _teamRegistrationService = teamRegistrationService;
        }

        public TeamRegisterResp CreateTeamAccount(TeamRegisterPost postInfo)
        {
            // Check If Username is Taken.
            var nameResult = _teamRegistrationService.IsUsernameValid(postInfo.Username);

            // Check password strength.
            var passwordResult = _teamRegistrationService.IsPasswordValid(postInfo.Password);

            // Check If Website url is taken. Then Check if valid and if its alive.
            var websiteUrl = _teamRegistrationService.IsWebsiteURLUnique(postInfo.WebsiteUrl);
            var websiteValid = _teamRegistrationService.IsUrlValid(postInfo.WebsiteUrl);
            var websiteAlive = _teamRegistrationService.IsUrlValid(postInfo.WebsiteUrl);

            // Check if  callback url is taken. Then Check if valid and if its alive.
            var callBackurl = _teamRegistrationService.IsCallBackURLUnique(postInfo.CallbackUrl);
            var callbackValid = _teamRegistrationService.IsUrlValid(postInfo.CallbackUrl);
            var callbackAlive = _teamRegistrationService.IsUrlAlive(postInfo.CallbackUrl);

            // If any of the above flags are false return json Object containing error info.
            if(!(nameResult && passwordResult && websiteUrl && websiteValid && websiteAlive && callBackurl && callbackValid && callbackAlive))
            {
                return new TeamRegisterResp(nameResult, passwordResult, websiteUrl, websiteValid, websiteAlive, callBackurl, callbackValid, callbackAlive, false);
            }
            else
            {
                // Create team if we pass all requirements.
                // generate salt for teams password.
                var salt = _teamRegistrationService.GenerateSalt(Constants.saltLength);

                // Create digest with salt.
                var digest = _teamRegistrationService.HashPassword(postInfo.Password, Constants.HashIteration, salt, KeyDerivationPrf.HMACSHA256);

                // Create Guid ClientId.
                var id = _teamRegistrationService.GenerateClientId();

                // Create Client Secret, client salt, and hash it.
                var clientSalt = _teamRegistrationService.GenerateSalt(Constants.SaltLength);
                var clientSecret = _teamRegistrationService.GenerateClientSecret();

                var clientDigest = _teamRegistrationService.HashPassword(clientSecret, Constants.HashIteration, clientSalt, KeyDerivationPrf.HMACSHA256);

                // Save the team in storage.
                var teamCreateResult = _teamRegistrationService.CreateTeam(postInfo, id, digest, clientDigest);

                // Return response with all flags corresponding flags.
                return new TeamRegisterResp(nameResult, passwordResult, websiteUrl, websiteValid, websiteAlive, callBackurl, callbackValid, callbackAlive, teamCreateResult, id, clientSecret);
            }
        }

    }
}

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
        private readonly UrlValidationService _urlValidationService;
        public TeamRegistrationManager(TeamRegistrationService teamRegistrationService, UrlValidationService urlValidationService)
        {
            _teamRegistrationService = teamRegistrationService;
            _urlValidationService = urlValidationService;
        }

        /// <summary>
        /// Create team accounts for the web api.
        /// </summary>
        /// <param name="postInfo">Json object to represent post request</param>
        /// <returns>Json response object</returns>
        public TeamRegisterResp CreateTeamAccount(TeamRegisterPost postInfo)
        {
            // Check If Username is Taken.
            var nameResult = _teamRegistrationService.IsUsernameValid(postInfo.Username);

            // Check password strength.
            var passwordResult = _teamRegistrationService.IsPasswordValid(postInfo.Password);

            // Check that passwords and repeat passwords are equal.
            passwordResult &= postInfo.Password == postInfo.RepeatPassword;

            // Check If Website url is taken. Then Check if valid, if its alive and its https.
            var websiteUrl = _urlValidationService.IsWebsiteURLUnique(postInfo.WebsiteUrl);
            var websiteValid = _urlValidationService.IsUrlValid(postInfo.WebsiteUrl);
            websiteValid &= _urlValidationService.IsUrlHttps(postInfo.WebsiteUrl);

            var websiteAlive = false;
            if (websiteValid)
            {
                websiteAlive = _urlValidationService.IsUrlAlive(postInfo.WebsiteUrl);
            }
            

            // Check if  callback url is taken. Then Check if valid, if its alive and its https.
            var callBackurl = _urlValidationService.IsCallBackURLUnique(postInfo.CallbackUrl);
            var callbackValid = _urlValidationService.IsUrlValid(postInfo.CallbackUrl);
            callbackValid &= _urlValidationService.IsUrlHttps(postInfo.CallbackUrl);

            var callbackAlive = false;
            if(callbackValid)
            {
                callbackAlive = _urlValidationService.IsUrlAlive(postInfo.CallbackUrl);

            }

            // If any of the above flags are false return json Object containing error info.
            if (!(nameResult && passwordResult && websiteUrl && websiteValid && websiteAlive && callBackurl && callbackValid && callbackAlive))
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

using API.AppConstants;
using API.Models.json;
using API.Services;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Protocols.WSTrust;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Managers
{
    public class TeamLoginManager
    {
        private readonly TeamLoginService _teamLoginService;
        private readonly JWTService _JWTService;
        public TeamLoginManager(TeamLoginService teamLoginService, JWTService jwtService)
        {
            _teamLoginService = teamLoginService;
            _JWTService = jwtService;

        }


        public TeamLoginResp TeamLogin(TeamLoginPost postInfo)
        {
            var userNameExist = _teamLoginService.CheckUsernameExistence(postInfo.Username);
            var passwordValid = false;
            
            // If the userNameExist then we grab the password
            if(userNameExist)
            {
                passwordValid = _teamLoginService.ValidatePassword(postInfo.Username, postInfo.Password, Constants.HashIteration, KeyDerivationPrf.HMACSHA256);
            }


            if (userNameExist && passwordValid)
            {
                // Grab ClientId to return to frontend.
                var clientId = _teamLoginService.GetClientIdFromUsername(postInfo.Username);
                return new TeamLoginResp()
                {
                    Status = true,

                    // TODO: updated audience
                    AccessToken = _JWTService.GenerateHmacSignedJWTToken(postInfo.Username, clientId, Constants.Issuer, DateTime.Now.ToUniversalTime(),
                                DateTime.Now.AddMinutes(Constants.AuthenticationValidMinutes).ToUniversalTime(), Constants.SigningKey),

                    Username = postInfo.Username,
                    ClientId = clientId
                };
            }
            else
            {
                return new TeamLoginResp() { Status = false, AccessToken = null, Username = null, ClientId = null };
            }

        }

    }
}

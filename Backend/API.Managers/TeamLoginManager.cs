﻿using API.AppConstants;
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

        /// <summary>
        /// Check if an authentication request is valid
        /// </summary>
        /// <param name="postInfo">Json object representing client credentials</param>
        /// <returns>Json response object</returns>
        public TeamLoginResp TeamLogin(TeamLoginPost postInfo)
        {
            // Check if the username exists.
            var userNameExist = _teamLoginService.CheckUsernameExistence(postInfo.Username);
            var passwordValid = false;
            
            // If the userNameExist then we grab the password.
            if(userNameExist)
            {
                passwordValid = _teamLoginService.ValidatePassword(postInfo.Username, postInfo.Password, Constants.HashIteration, KeyDerivationPrf.HMACSHA256);
            }

            // If authentication passes return the corresponding json response.
            if (userNameExist && passwordValid)
            {
                // Grab ClientId to return to frontend.
                var clientId = _teamLoginService.GetClientIdFromUsername(postInfo.Username);
                return new TeamLoginResp()
                {
                    Status = true,

                    // Access token to authorize protected resources.
                    AccessToken = _JWTService.GenerateHmacSignedJWTToken(Constants.Issuer, clientId, Constants.Issuer, DateTime.Now.ToUniversalTime(),
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

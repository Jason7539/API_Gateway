﻿using API.Models.gateway;
using System;
using System.Linq;
using API.AppConstants;
using System.Net;
using System.Net.Http;
using API.Models.json;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Threading.Tasks;

namespace API.Services
{
    public class TeamRegistrationService
    {

        public bool IsUsernameValid(string username)
        {
            using (var context = new ApiGatewayContext())
            {
                // Attempt to find a team with inputted username.
                var teamNames = context.Team
                                        .Where(team => username == team.Username)
                                        .ToList();

                // Return true if we couldn't find a match. And test for the length of the username.
                return teamNames.Count == 0 && username.Length >= Constants.UsernameMin && username.Length < Constants.UsernameMax;
            }
        }


        public bool CreateTeam(TeamRegisterPost postInfo, string clientId, byte[] digest, byte[] clientDigest)
        {
            try
            {
                using (var context = new ApiGatewayContext())
                {
                    // Create team model.
                    var newTeam = new Team() { ClientId = clientId, Secret = Convert.ToBase64String(clientDigest), WebsiteUrl = postInfo.WebsiteUrl, Digest = Convert.ToBase64String(digest), CallbackUrl = postInfo.CallbackUrl, Username = postInfo.Username };

                    // Save team.
                    context.Team.Add(newTeam);
                    context.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }

        }

        public byte[] GenerateSalt(int saltLength)
        {
            using(RNGCryptoServiceProvider randomGen = new RNGCryptoServiceProvider())
            {
                var salt = new byte[saltLength];

                randomGen.GetBytes(salt);
                return salt;
            }
        }

        public byte[] HashPassword(string password, int iteration, byte[] salt, KeyDerivationPrf function)
        {
            // Use Pbkdf2 on password and salt to create key.
            var key = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: function,
                iterationCount: iteration,
                numBytesRequested: Constants.HashLength);

            // Create hash with the size of salt and key.
            var hashed = new byte[Constants.HashLength + Constants.saltLength];

            // Copy the salt to the hashed array.
            Array.Copy(salt, 0, hashed, 0, Constants.saltLength);

            // Copy the key into the hashed array.
            Array.Copy(key, 0, hashed, Constants.saltLength, Constants.HashLength);

            return hashed;
        }

        public string GenerateClientId()
        {
            return Guid.NewGuid().ToString();
        }

        public string GenerateClientSecret()
        {
            // Generate random password.
           return Convert.ToBase64String(GenerateSalt(Constants.saltLength));
        }

        public bool IsPasswordValid(string password)
        {
            // Check password size.
            if (password.Length > Constants.PasswordMax || password.Length < Constants.PasswordMin)
            {
                return false;
            }

            // Check sequence for password.


            return true;
        }
    }
}

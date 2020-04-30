using API.AppConstants;
using API.Models.gateway;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.Services
{
    public class TeamLoginService
    {
        private readonly ApiGatewayContext _context;

        public TeamLoginService(ApiGatewayContext apiGatewayContext)
        {
            _context = apiGatewayContext;
        }

        /// <summary>
        /// Test if a team's username exists
        /// </summary>
        /// <param name="username">username to test</param>
        /// <returns>Bool representing whether the username exists</returns>
        public bool CheckUsernameExistence(string username)
        {
  
            // Attempt to find a team with inputted username.
            var teamNames = _context.Team
                                    .Where(team => team.Username == username)
                                    .ToList();

            return teamNames.Count > 0;
            
        }

        /// <summary>
        /// Validate a team's password credentials
        /// </summary>
        /// <param name="username">Team's username</param>
        /// <param name="password">Team's password</param>
        /// <param name="iteration">Iteration used to recalc digest</param>
        /// <param name="function">Hash function to calc digest</param>
        /// <returns></returns>
        public bool ValidatePassword(string username, string password, int iteration, KeyDerivationPrf function)
        {
            // Get the digest for that user.    
            var teamDigestString = _context.Team
                                    .Where(team => team.Username == username)
                                    .Select(team =>  new {digest = team.Digest })
                                    .ToList().ElementAt(0).digest;

            // Turn the digest into byte[].
            var teamDigestByte = Convert.FromBase64String(teamDigestString);

            // Extract salt from digest.
            var salt = new byte[Constants.SaltLength];
            Array.Copy(teamDigestByte, 0, salt, 0, Constants.SaltLength);

            // Extrash hash from digest.
            var hash = new byte[Constants.HashLength];
            Array.Copy(teamDigestByte, Constants.SaltLength, hash, 0, Constants.HashLength);

            // Recalc digest for inputted password.
            var recalcDigest = KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: function,
                    iterationCount: iteration,
                    numBytesRequested: Constants.HashLength);

            // Check if recalc digest is equal to the hash in data store.
            return recalcDigest.SequenceEqual(hash);
        }

        /// <summary>
        /// Retrieve the username for a team given the team's client id
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public string GetClientIdFromUsername(string username)
        {
            // Get a teams ClientId using its username.
            var ClientIds = _context.Team
                            .Where(team => username == team.Username)
                            .Select(team => new { team.ClientId })
                            .ToList()[0].ClientId;
            return ClientIds;
        }

    }
}

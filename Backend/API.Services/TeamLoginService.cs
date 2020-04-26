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
        public bool CheckUsernameExistence(string username)
        {
            using (var context = new ApiGatewayContext())
            {
                // Attempt to find a team with inputted username.
                var teamNames = context.Team
                                        .Where(team => team.Username == username)
                                        .ToList();

                return teamNames.Count > 0;
            }
        }

        public bool ValidatePassword(string username, string password, int iteration, KeyDerivationPrf function)
        {
            
            using (var context = new ApiGatewayContext())
            {
                // Get the digest for that user.    
                var teamDigestString = context.Team
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


        }

        public string GetClientIdFromUsername(string username)
        {
            using(var context = new ApiGatewayContext())
            {
                // Get a teams ClientId using its username.
                var ClientIds = context.Team
                                .Where(team => username == team.Username)
                                .Select(team => new { team.ClientId })
                                .ToList()[0].ClientId;
                return ClientIds;
            }

        }

    }
}

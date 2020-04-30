using API.Models.gateway;
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
        private readonly ApiGatewayContext _context;
        public TeamRegistrationService(ApiGatewayContext apiGatewayContext)
        {
            _context = apiGatewayContext;
        }

        /// <summary>
        /// Test if a username is valid
        /// </summary>
        /// <param name="username">Username to test</param>
        /// <returns>Bool representing whether the username is valid</returns>
        public bool IsUsernameValid(string username)
        {
            // Attempt to find a team with inputted username.
            var teamNames = _context.Team
                                    .Where(team => username == team.Username)
                                    .ToList();

            // Return true if we couldn't find a match. And test for the length of the username.
            return teamNames.Count == 0 && username.Length >= Constants.UsernameMin && username.Length < Constants.UsernameMax;
            
        }

        /// <summary>
        /// Create a team in the data store
        /// </summary>
        /// <param name="postInfo">Json request object for creating team</param>
        /// <param name="clientId">Client id for team to create</param>
        /// <param name="digest">Digest to store for the team</param>
        /// <param name="clientDigest">Client digest used for Oauth flow</param>
        /// <returns></returns>
        public bool CreateTeam(TeamRegisterPost postInfo, string clientId, byte[] digest, byte[] clientDigest)
        {
            try
            {
                // Create team model.
                var newTeam = new Team() { ClientId = clientId, Secret = Convert.ToBase64String(clientDigest), WebsiteUrl = postInfo.WebsiteUrl, Digest = Convert.ToBase64String(digest), CallbackUrl = postInfo.CallbackUrl, Username = postInfo.Username };

                // Save team.
                _context.Team.Add(newTeam);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Generate random bytes used for salting
        /// </summary>
        /// <param name="saltLength">Length of the salt</param>
        /// <returns>Byte array containing salt</returns>
        public byte[] GenerateSalt(int saltLength)
        {
            using(RNGCryptoServiceProvider randomGen = new RNGCryptoServiceProvider())
            {
                var salt = new byte[saltLength];

                // Generate and fill byte array.
                randomGen.GetBytes(salt);
                return salt;
            }
        }

        /// <summary>
        /// Hash a password using PBKDF2
        /// </summary>
        /// <param name="password">Plain text password to hash</param>
        /// <param name="iteration">Amount of iteration</param>
        /// <param name="salt">Salt used in hashing</param>
        /// <param name="function">Key derivation function</param>
        /// <returns>Byte array of hash</returns>
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

        /// <summary>
        /// Create a client id guid used in oauth flow
        /// </summary>
        /// <returns>String of the guid</returns>
        public string GenerateClientId()
        {
            return Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Create client secret used in oauth flow
        /// </summary>
        /// <returns>String of client secret</returns>
        public string GenerateClientSecret()
        {
           return Convert.ToBase64String(GenerateSalt(Constants.saltLength));
        }

        /// <summary>
        /// Test if a teams password is valid
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
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

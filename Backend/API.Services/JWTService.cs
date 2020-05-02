using API.AppConstants;
using Microsoft.IdentityModel.Protocols.WSTrust;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;

namespace API.Services
{
    public class JWTService
    {
        // https://tools.ietf.org/html/rfc7518#section-3.2 
        // Key same size has hash algorithms output.

        /// <summary>
        /// Creates a encoded and signedd JWT.
        /// </summary>
        /// <param name="auidience">Specify audience claim</param>
        /// <param name="scope">Specify scope claim</param>
        /// <param name="issuer">Specify issuer claim</param>
        /// <param name="issuedAt">Specify issuedAt claim</param>
        /// <param name="expiredAt">Specify expiredAt claim</param>
        /// <param name="key">Key to sign the JWT</param>
        /// <returns>String representing jwt token</returns>
        public string GenerateHmacSignedJWTToken(string auidience, string scope, string issuer, DateTime issuedAt, DateTime expiredAt, string key)
        {
            // Create token handler to create JWT.
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            
            // Retrieve key used for JWT signing.
            var Signingkey = Convert.FromBase64String(key);

            // Add scope claim for the JWT.
            var payload = new ClaimsIdentity( new Claim[]{
                new Claim(Constants.Scope, scope),
            });

            // Signing information for the JWT.
            var signingCredential = new SigningCredentials(new SymmetricSecurityKey(Signingkey), SecurityAlgorithms.HmacSha256Signature);

            // Return the encoded JWT.
            return jwtTokenHandler.CreateEncodedJwt(issuer, auidience, payload, null, expiredAt, issuedAt, signingCredential);
        }

        /// <summary>
        /// Validates a JWT token
        /// </summary>
        /// <param name="token">String representation of the token</param>
        /// <returns>bool representing whether the token is valid</returns>
        public bool ValidateHmacSignedJWTToken(string token)
        {
            try
            {
                // Object to specify validation requirements.
                var validationReq = new TokenValidationParameters
                {
                    // Signing key to test.
                    IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(Constants.SigningKey)),

                    // Flag to specify what to test.
                    ValidateIssuerSigningKey = true,
                    RequireSignedTokens = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidIssuer = Constants.Issuer
                };

                var handler = new JwtSecurityTokenHandler();
                var outToken = handler.ReadToken(token);

                // Perform validation operation. This method will throw an exception if token invalid.
                handler.ValidateToken(token, validationReq, out outToken);

                return true;
            }
            catch 
            {
                return false;
            }
        }
    }
}

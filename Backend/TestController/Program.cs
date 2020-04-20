using System;
using TestController.sakila;
using API.Models.gateway;
using System.Linq;
using API.Services;
using System.Security.Cryptography.X509Certificates;
using API.AppConstants;
using Microsoft.VisualBasic.CompilerServices;
using System.Security.Cryptography;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using API.Managers;
using API.Models.json;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TestController
{
    class Program
    {
        static void Main(string[] args)
        {
            //using var context = new ApiGatewayContext();
            //var teamName = context.Team

            var tls = new TeamLoginService();

            var username = "JASONJASON";
            var pw = "passwordpassword123321";

            Console.WriteLine("testing if username good: " + tls.CheckUsernameExistence("asda"));
            Console.WriteLine("testing if password is good " + tls.ValidatePassword(username, pw, Constants.HashIteration, KeyDerivationPrf.HMACSHA256));


            // Create the Signing key             using(RNGCryptoServiceProvider randomGen = new RNGCryptoServiceProvider())
            //using (RNGCryptoServiceProvider randomGen = new RNGCryptoServiceProvider())
            //{ 
            //    var signingKey = new byte[32];

            //    randomGen.GetBytes(signingKey);
            //    Console.WriteLine("key is");
            //    Console.WriteLine(Convert.ToBase64String(signingKey));

            //}



            var jwtService = new JWTService();
        

            var token = jwtService.GenerateHmacSignedJWTToken("myClient", "read", Constants.Issuer, DateTime.Now.ToUniversalTime(), DateTime.Now.AddDays(10).ToUniversalTime(),
                                                    Constants.SigningKey);
            // now we have string repre of token lets validate it

            var handler = new JwtSecurityTokenHandler();

            // create fake key.
            


            //var validationReq = new TokenValidationParameters();
            //validationReq.IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("asdasds"));
            //validationReq.ValidAudience = "myClient";
            //validationReq.ValidateAudience = true;
            //validationReq.ValidIssuer = Constants.Issuer;

            //validationReq.ValidateIssuerSigningKey = true;
            //validationReq.RequireSignedTokens = true;
            //validationReq.ValidateIssuer = true;
            //validationReq.ValidateLifetime = true;

            //Console.WriteLine(token);

            //var evilToken = token + "asdasd";


            //var outToken = handler.ReadToken(token);


            //var result = handler.ValidateToken(token, validationReq, out outToken);

            //Console.WriteLine(result);
        }

    }
}

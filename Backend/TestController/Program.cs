﻿using System;
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



            /////////////////// MAKING TOKENS /////////////////
            //var jwtService = new JWTService();    

            //var token = jwtService.GenerateHmacSignedJWTToken("myClient", "read", Constants.Issuer, DateTime.Now.ToUniversalTime(), DateTime.Now.AddDays(10).ToUniversalTime(),
            //                                        Constants.SigningKey);
            //// now we have string repre of token lets validate it

            //var handler = new JwtSecurityTokenHandler().ReadJwtToken(token);

            //var claims = handler.Claims.Where(x => x.Type == "aud").FirstOrDefault().Value;


            ////foreach (var item in claims)
            ////{
            ////    Console.WriteLine(item);
            ////}
            //Console.WriteLine(claims);
            //// create fake key.

            //Console.WriteLine(token);



            /////////////////////// TESTING TOKEN VALIDITY /////////////////////////
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



            //////////////////////// RANDOM TEST /////////////////
            var testService = new TestService(new ApiGatewayContext());
            Console.WriteLine(testService.GetTeams("JASONJASON"));

            Console.WriteLine(testService.CheckIfServiceOwner("JASONJASON", "/gingmygoo"));

        }

    }
}

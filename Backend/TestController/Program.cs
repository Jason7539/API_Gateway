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

namespace TestController
{
    class Program
    {
        static void Main(string[] args)
        {
            //using var context = new ApiGatewayContext();
            //var teamName = context.Team
            //                .Where(s => s.ClientId == "Bill")
            //                .ToList();

            //foreach (var name in teamName)
            //{
            //    Console.WriteLine(name.ClientId);
            //}

            // Creating a team.

            using var context = new ApiGatewayContext();
            var query = context.Team
                        .Where(s => s.Username == "billybob");





            var trs = new TeamRegistrationService();
            //var salt = trs.GenerateSalt(Constants.saltLength);

            //var saltBase = Convert.ToBase64String(salt);

            //var password = "pooppoop";

            //var hashedPassword = trs.HashPassword(password, 10000, salt, KeyDerivationPrf.HMACSHA256);


            //Console.WriteLine("saltBase " + saltBase);

            //Console.WriteLine("hashPW: " + Convert.ToBase64String(hashedPassword));

            //// We Have Saved the password in the database
            //// now we grab it out.
            //var saltret = new byte[(Constants.saltLength)];
            //var retDigest = new byte[(Constants.HashSize)];

            //Array.Copy(hashedPassword, 0, saltret, 0, Constants.saltLength);
            //Array.Copy(hashedPassword, Constants.saltLength, retDigest, 0, Constants.HashSize);

            //Console.WriteLine("retrieve salt is:   " + Convert.ToBase64String(saltret));

            //Console.WriteLine("are salt equal");
            //Console.WriteLine(salt.SequenceEqual(saltret));


            //// Calculate digest.
            //var key = KeyDerivation.Pbkdf2(
            //    password: password,
            //    salt: salt,
            //    prf: KeyDerivationPrf.HMACSHA256,
            //    iterationCount: 10000,
            //    numBytesRequested: Constants.HashSize);
            //Console.WriteLine("digest cal is " + Convert.ToBase64String(key));

            //// retrieve digest.
            //Console.WriteLine("retrieve dige " + Convert.ToBase64String(retDigest));

            //Console.WriteLine("are digest equal");
            //Console.WriteLine(key.SequenceEqual(retDigest));


            //Store it in DB and retrieve again
            //var asdf = Convert.ToBase64String(hashedPassword);
            //var team = new Team() { ClientId = "asd", CallbackUrl = "asdasd", Digest = asdf, Secret = "asda", Username = "asdsad", WebsiteUrl = "asdasd" };

            //context.Team.Add(team);
            //context.SaveChanges();

            //Console.WriteLine("stored: " + asdf);


            // Verify password and hash are legit.
            //var team = context.Team
            //            .Where(team => team.ClientId == "asd")
            //            .ToList();

            //var hashpw = Convert.FromBase64String(team[0].Digest);

            //salt = new byte[Constants.SaltLength];

            //// copy salt over
            //Array.Copy(hashpw, 0, salt, 0, Constants.SaltLength);

            //// calculat digest.
            //var recalcDigest = KeyDerivation.Pbkdf2(
            //    password: password,
            //    salt: salt,
            //    prf: KeyDerivationPrf.HMACSHA256,
            //    iterationCount: 10000,
            //    numBytesRequested: Constants.HashSize);

            //var retrievedDigest = new byte[Constants.HashSize];

            //Array.Copy(hashpw, Constants.saltLength, retrievedDigest, 0, Constants.HashSize);

            //Console.WriteLine("recalcDigest " +Convert.ToBase64String(recalcDigest));
            //Console.WriteLine("retrivDigest " +Convert.ToBase64String(retrievedDigest));


            /////////////////////// TESTING teamREgistration MANAGER ///////////////
            // Test name uniqueness
            var username = "jasonjason";
      
            
            // Attempt to find a team with inputted username.
            var teamNames = context.Team
                                    .Where(s => s.Username == "jacoasdf")
                                    .ToList();

            // Return true if we couldn't find a match. And test for the length of the username.
            var result = (teamNames.Count == 0) && username.Length > Constants.UsernameMin && username.Length < Constants.UsernameMax;
            Console.WriteLine(teamNames.Count);

            Console.WriteLine(result);

        }
    }
}

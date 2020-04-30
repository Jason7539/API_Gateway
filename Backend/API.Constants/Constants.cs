using System;

namespace API.AppConstants
{
    public static class Constants
    {
        // ENVIRONMENT VARIABLES
        public static readonly string SigningKey = Environment.GetEnvironmentVariable("SIGNING_KEY", EnvironmentVariableTarget.User);
        public static readonly string SQLConnection = Environment.GetEnvironmentVariable("API_SQL_CONNECTION", EnvironmentVariableTarget.User);



        // TEAM REGISTRATION
        public static readonly int UsernameMin = 4;
        public static readonly int UsernameMax = 200;
        public static readonly int PasswordMin = 12;
        public static readonly int PasswordMax = 2000;
        public static readonly int SaltLength = 32;
        public static readonly string HttpHEAD = "HEAD";
        public static readonly int HashLength = 32;
        public static readonly int HashIteration = 10000;
        public static readonly int saltLength = 32;

        // JWT INFORMATION
        public static readonly string Issuer = "Spring2020APIGateway";      // TODO: update to api domain.
        public static readonly string ClientId = "ClientId";
        public static readonly string Scope = "Scope";
        public static readonly int AuthenticationValidMinutes = 30;

        // SERVICE MANAGEMENT
        public static readonly int ServiceManagementPagination = 2;

    }
}

using System;

namespace API.AppConstants
{
    public static class Constants
    {

        public static readonly string APISQLConnection = Environment.GetEnvironmentVariable("PRIVATE_KEY", EnvironmentVariableTarget.User);



        // TEAM REGISTRATION
        public static readonly int UsernameMin = 4;
        public static readonly int UsernameMax = 200;
        public static readonly int PasswordMin = 12;
        public static readonly int PasswordMax = 2000;
        public static readonly int SaltLength = 32;
        public static readonly string HttpHEAD = "HEAD";
        public static readonly int HashSize = 32;
        public static readonly int HashIteration = 10000;
        public static readonly int saltLength = 32;

    }
}

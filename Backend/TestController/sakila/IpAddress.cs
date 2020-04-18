using System;
using System.Collections.Generic;

namespace TestController.sakila
{
    public partial class IpAddress
    {
        public string Ip { get; set; }
        public long TimestampLocked { get; set; }
        public int RegistrationFailures { get; set; }
        public long LastRegFailTimestamp { get; set; }
    }
}

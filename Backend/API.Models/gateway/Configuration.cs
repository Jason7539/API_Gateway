using System;
using System.Collections.Generic;

namespace API.Models.gateway
{
    public partial class Configuration
    {
        public string Steps { get; set; }
        public string OpenTo { get; set; }
        public string EndPoint { get; set; }

        public virtual Service EndPointNavigation { get; set; }
        public virtual Team OpenToNavigation { get; set; }
    }
}

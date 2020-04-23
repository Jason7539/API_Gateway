using System;
using System.Collections.Generic;

namespace API.Models.gateway
{
    public partial class Team
    {
        public Team()
        {
            Configuration = new HashSet<Configuration>();
            Service = new HashSet<Service>();
        }

        public string ClientId { get; set; }
        public string WebsiteUrl { get; set; }
        public string Secret { get; set; }
        public string CallbackUrl { get; set; }
        public string Digest { get; set; }
        public string Username { get; set; }

        public virtual ICollection<Configuration> Configuration { get; set; }
        public virtual ICollection<Service> Service { get; set; }
    }
}

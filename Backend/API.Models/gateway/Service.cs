using System;
using System.Collections.Generic;

namespace API.Models.gateway
{
    public partial class Service
    {
        public Service()
        {
            Configuration = new HashSet<Configuration>();
        }

        public string Endpoint { get; set; }
        public string Owner { get; set; }
        public int Id { get; set; }
        public string Input { get; set; }
        public string Output { get; set; }
        public string Dataformat { get; set; }
        public string Description { get; set; }

        public virtual Team OwnerNavigation { get; set; }
        public virtual ICollection<Configuration> Configuration { get; set; }
    }
}

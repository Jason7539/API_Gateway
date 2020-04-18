using System;
using System.Collections.Generic;

namespace API.Models.gateway
{
    public partial class Service
    {
        public string Endpoint { get; set; }
        public string Owner { get; set; }
        public int Id { get; set; }

        public virtual Team OwnerNavigation { get; set; }
    }
}

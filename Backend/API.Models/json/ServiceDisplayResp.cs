using API.Models.gateway;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Models.json
{
    public class ServiceDisplayResp
    {
        public ICollection<Service> Services { get; set; }
    }
}

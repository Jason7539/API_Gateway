using API.Models.gateway;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Models.json
{
    public class ServiceDisplayResp
    {
        public string Endpoint { get; set; }
        public string Username { get; set; }
        public string Input { get; set; }
        public string Output { get; set; }
        public string Dataformat { get; set; }
        public string Description { get; set; }


    }
}

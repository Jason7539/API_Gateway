using System;
using System.Collections.Generic;
using System.Text;

namespace API.Models.json
{
    public class CreateServicePost
    {
        public bool Status { get; set; }
        public string Username { get; set; }
        public string ClientId { get; set; }
        public string RouteToAccess { get; set; }
        public string ServiceDescription { get; set; }
        public List<string> OpenTo { get; set; }
        public string Configurations { get; set; }
        public string Input { get; set; }
        public string Output { get; set; }
        public string DataFormat { get; set; }

    }
}

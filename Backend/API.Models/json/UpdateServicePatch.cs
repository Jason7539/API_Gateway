using System;
using System.Collections.Generic;
using System.Text;

namespace API.Models.json
{
    public class UpdateServicePatch
    {
        public string ClientId { get; set; }
        public string EndPoint { get; set; }
        public List<string> OpenTo { get; set; }
    }
}

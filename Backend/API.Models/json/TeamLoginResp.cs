using System;
using System.Collections.Generic;
using System.Text;

namespace API.Models.json
{
    public class TeamLoginResp
    {
        public string ClientId { get; set; }
        public bool Status { get; set; }
        public string AccessToken{ get; set; }
        public string Username{ get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace API.Models.json
{
    public class TeamRegisterPost
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string WebsiteUrl { get; set; }

        public string  CallbackUrl { get; set; }
    }
}

using API.Models.gateway;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Models.json
{
    public class TeamRegisterResp
    {
        public bool NameUnique { get; set; }
        public bool PasswordValid { get; set; }
        public bool WebsiteUnique { get; set; }
        public bool WebsiteValid { get; set; }
        public bool WebsiteAlive { get; set; }
        public bool CallbackUnique { get; set; }
        public bool CallbackValid { get; set; }
        public bool CallbackAlive { get; set; }

        public bool TeamCreate { get; set; }


        public TeamRegisterResp(bool nameUnique, bool passwordValid, bool websiteUnique, bool websiteValid, bool websiteAlive,
                                bool callbackUnique, bool callbackValid, bool callbackAlive, bool teamCreate)
        {
            NameUnique = nameUnique;
            PasswordValid = passwordValid;
            WebsiteUnique = websiteUnique;
            WebsiteValid = websiteValid;
            WebsiteAlive = websiteAlive;
            CallbackUnique = callbackUnique;
            CallbackValid = callbackValid;
            CallbackAlive = callbackAlive;
            TeamCreate = teamCreate;
        }
    }
}

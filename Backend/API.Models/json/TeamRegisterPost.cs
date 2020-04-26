using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace API.Models.json
{
    public class TeamRegisterPost
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
        
        [Required]
        public string RepeatPassword { get; set; }


        [Required]
        public string WebsiteUrl { get; set; }

        [Required]
        public string  CallbackUrl { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace API.Models.json
{
    public class CreateServicePost
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string ClientId { get; set; }
        [Required]
        public string RouteToAccess { get; set; }
        [Required]
        public string ServiceDescription { get; set; }
        [Required]
        public List<string> OpenTo { get; set; }
        [Required]
        public string Configurations { get; set; }
        [Required]
        public string Input { get; set; }
        [Required]
        public string Output { get; set; }
        [Required]
        public string DataFormat { get; set; }

    }
}


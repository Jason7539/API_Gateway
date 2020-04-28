using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace API.Models.json
{
    public class UpdateServicePatch
    {
        [Required]
        public string ClientId { get; set; }
        [Required]
        public string EndPoint { get; set; }
        [Required]
        public List<string> OpenTo { get; set; }
    }
}

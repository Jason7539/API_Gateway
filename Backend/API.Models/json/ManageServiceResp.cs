﻿using System;
using System.Collections.Generic;
using System.Text;

namespace API.Models.json
{
    public class ManageServiceResp
    {
        public string Endpoint { get; set; }
        public string Input { get; set; }
        public string Output { get; set; }
        public string DataFormat { get; set; }
        public string Description { get; set; }
    }
}

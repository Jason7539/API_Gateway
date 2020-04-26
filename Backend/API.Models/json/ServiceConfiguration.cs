using System;
using System.Collections.Generic;
using System.Text;

namespace API.Models.json
{
    public class ServiceConfiguration
    {
        public int Steps { get; set; }
        public int ReturnStep { get; set; }

        public List<Configuration> Configurations { get; set; }

        public class Configuration
        {
            public string Action { get; set; }
            public string ParameterNames { get; set; }
            public string ParameterDataTypes { get; set; }
            public string HttpMethod { get; set; }
            public bool Async { get; set; }
        }

    }
}

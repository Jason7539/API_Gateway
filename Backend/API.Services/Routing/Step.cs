using System;
using System.Collections.Generic;
using System.Text;

namespace API.Services
{
    class Step : IStep
    {
        public IStep Next { get; set; }
        public bool Async { get; set; }
        public string Action { get; set; }
        public bool OutputRequired { get; set; }
        public string[] ArrayParameterTypes { get; set; }
        public string[] ArrayParameterNames { get; set; }
        public string HttpMethod { get; set; }


        public Step()
        { }

        public void Execute()
        {

        }
    }
}

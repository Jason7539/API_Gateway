using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace API.Services
{
    public class Step : IStep
    {
        //public IStep Next { get; set; }
        public bool Async { get; set; }
        //public string Action { get; set; }
        //public bool OutputRequired { get; set; }
        public string[] ArrayParameterTypes { get; set; }
        public string[] ArrayParameterNames { get; set; }
        public IDictionary<string,object> ParameterPairings
        { get; set; }
        public HttpRequestMessage Message { get; set; }

        public Step()
        {
            ParameterPairings = new ExpandoObject();
        }

        public HttpContent CreateContent(dynamic content)
        {
            HttpContent httpContent = null;
            if (content is object)
            {


                httpContent.Headers.Add("Accept","application/json");
            }

            return httpContent;
        }

        public dynamic ExecuteStep(dynamic pastResult)
        {
            using (var client = new HttpClient())
            {
                var content = CreateContent(pastResult);
                //ReferenceEquals(null,DynamicObjectName)  == nullcheck

                if(!Async)
                {

                    //sync code here


                }


                //async code here
                return null;
            }




        }

    }
}

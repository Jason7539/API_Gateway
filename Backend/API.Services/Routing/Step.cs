using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace API.Services
{
    public class Step : IStep
    {
        //public IStep Next { get; set; }
        public bool Async { get; set; }
        public string Action { get; set; }
        //public bool OutputRequired { get; set; }
        public string[] ArrayParameterTypes { get; set; }
        public string[] ArrayParameterNames { get; set; }
        public string HttpMethod { get; set; }
        //public IDictionary<string,object> ParameterPairings{ get; set; }
        public HttpRequestMessage Message { get; set; }

        public Step()
        {
            //ParameterPairings = new ExpandoObject();
        }

        /// <summary>
        /// Private method that scans the previous result of the steps and attempts to find any matching parameters to create the next json with 
        /// 
        /// </summary>
        /// <param name="content"></param>
        public StringContent CreateContent(string content)
        {

            if (content != "")
            {
                var dictionary = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(content);

                foreach (var item in dictionary)
                {
                    if (Array.IndexOf(ArrayParameterNames, item.Key) > 0)
                    {
                        var index = Array.IndexOf(ArrayParameterNames, item.Key);

                    }
                }


                var usableParams = JsonConvert.SerializeObject(dictionary);
                return new StringContent(usableParams, Encoding.UTF8, "application/json");
            }
            return null;

        }


        public string ExecuteStep(string pastResult)
        {
            using (var client = new HttpClient())
            {
                var requestContent = CreateContent(pastResult);

                if (!Async)
                {

                    //sync code here


                }


                //async code here
                return null;
            }




        }





    }
}

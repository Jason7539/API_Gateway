using API.Models.gateway;
using Microsoft.AspNetCore.Http;
using Microsoft.Diagnostics.Tracing.Parsers.MicrosoftWindowsWPF;
using Microsoft.IdentityModel.Protocols.WSTrust;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;




namespace API.Services
{
    /// <summary>
    /// Builder for creating a linked list of steps that can use fire and forget async to increase processing speed
    /// </summary>
    public class LLFireForgetBuilder : ILLBuilder
    {
        public LLFireForgetBuilder() { }
        
        public ILLRouter Build(HttpRequest initialRequest, string clientId)
        {
            string configData;
            var action = initialRequest.Path;

            using (var context = new ApiGatewayContext())
            {
                configData = context.Configuration.Where(con => CompareActions(action, con.EndPoint.ToString())
                && con.OpenTo.Equals(clientId)).Select(con => con.Steps).ToString();
                var stepColumn = JObject.Parse(configData);

                //retrieving the data from the stepColumn of sql

                var returnStep = (int)stepColumn.GetValue("ReturnStep");
                var configArray = stepColumn.GetValue("Configurations").ToString();

                var array = JArray.Parse(configArray);
                var fireForgetRouter = new LLFireForgetRouterAsync(array.Count)
                {
                    InitialRequest = initialRequest,
                    ReturnStep = returnStep,
                };
                var iter = 0;
                foreach (JObject setup in array)
                {
                    IStep step = new Step
                    {
                        Async = (bool)setup.GetValue("Async"),
                        ArrayParameterTypes = setup.GetValue("ParameterDataTypes").ToObject<string[]>(),
                        ArrayParameterNames = setup.GetValue("ParameterNames").ToObject<string[]>(),
                        Action = setup.GetValue("Action").ToString(),
                        HttpMethod = setup.GetValue("HttpMethod").ToString()
                    };
                    fireForgetRouter.Steps[iter] = step;
                    iter++;
                }

                return fireForgetRouter;
            }
        }

        /// <summary>
        /// This method takes the initial requests uri and compares it to the dbquery stored uri to see if it is a match
        /// Method allows for storage of path parameter based services in the future 
        /// </summary>
        /// <param name="request">Original Request uri</param>
        /// <param name="dBaction">db query uri</param>
        /// <returns>true if applicable, false if not</returns>
        private bool CompareActions(string request, string dBaction)
        {
            //trim off query parameters
            if(request.IndexOf("?")> 0)
            {
                request = request.Substring(0, request.IndexOf("?"));
            }
            if(dBaction.IndexOf("?")>0)
            {
                dBaction = dBaction.Substring(0, dBaction.IndexOf("?"));
            }
            char[] separator = { '/' };
            string[] requestSplit =request.Split(separator);
            string[] dBactionSplit = dBaction.Split(separator);

            if (requestSplit.Length != dBactionSplit.Length)
                return false;
            for (int i = 0; i < dBactionSplit.Length; i++)
            {
                if (dBactionSplit[i].Contains('{') && dBactionSplit[i].Contains('}'))
                    continue;
                if (!dBactionSplit[i].Equals(requestSplit[i]))
                    return false;
            }
            return true;
        }

       


    }
}

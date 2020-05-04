using API.Models.gateway;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Protocols.WSIdentity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace API.Services
{
    /// <summary>
    /// Builder for creating a linked list of steps that can use fire and forget async to increase processing speed
    /// </summary>
    public class LLFireForgetBuilder : ILLBuilder
    {
        public LLFireForgetBuilder() { }

        public ILLRouter Build(String authContext, string serviceConfigId)
        {

            //fix how we find the client id here so we can compare properly
            var clientID = authContext.User.FindFirst("ClientID")?.Value;
            string configData;
            using (var context = new ApiGatewayContext())
            {
                configData = context.Configuration.Where(con => con.EndPoint.Equals(serviceConfigId)&& con.OpenTo.Equals(clientID)).Select(con=>con.Steps).ToString();
            }
            //retrieving the data from the stepColumn of sql
            var stepColumn = JObject.Parse(configData);
            var returnStep = (int)stepColumn.GetValue("ReturnStep");
            var configArray = stepColumn.GetValue("Configurations").ToString();

            var array = JArray.Parse(configArray);
            var fireForgetRouter = new LLFireForgetRouterAsync(array.Count)
            {
                ReturnStep = returnStep,
                CallbackUrl = serviceConfigId
            };
            var iter = 0;
            foreach(JObject setup in array)
            {
                IStep step = new Step
                {
                    Async = (bool)setup.GetValue("Async"),
                    //OutputRequired = (bool)setup.GetValue("OutputRequired"),
                    //
                    ArrayParameterTypes = setup.GetValue("ParameterDataTypes").ToObject<string[]>(),
                    ArrayParameterNames = setup.GetValue("ParameterNames").ToObject<string[]>()
                };
                var action = setup.GetValue("Action").ToString();
                var method = setup.GetValue("HttpMethod").ToString();
                step.Message = CraftHttpRequestMessage(method, action);

                fireForgetRouter.Steps[iter] = step;
                iter++;
            }

            return fireForgetRouter;
        }

        private HttpRequestMessage CraftHttpRequestMessage(string methodType, string action)
        {
            HttpRequestMessage message;
            switch (methodType)
            {
                case "HEAD":
                    message = new HttpRequestMessage(HttpMethod.Head, action);
                    break;
                case "GET":
                    message = new HttpRequestMessage(HttpMethod.Get, action);
                    break;
                case "POST":
                    message = new HttpRequestMessage(HttpMethod.Post, action);
                    break;
                case "PUT":
                    message = new HttpRequestMessage(HttpMethod.Put, action);
                    break;
                case "DELETE":
                    message = new HttpRequestMessage(HttpMethod.Delete, action);
                    break;
                case "OPTIONS":
                    message = new HttpRequestMessage(HttpMethod.Options, action);
                    break;
                case "TRACE":
                    message = new HttpRequestMessage(HttpMethod.Trace, action);
                    break;
                default:
                    throw new InvalidCastException("MethodType was not a proper type");
            }
            return message;
        }



    }
}

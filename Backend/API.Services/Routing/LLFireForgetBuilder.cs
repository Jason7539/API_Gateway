using API.Models.gateway;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.Services
{
    /// <summary>
    /// Builder for creating a linked list of steps that can use fire and forget async to increase processing speed
    /// </summary>
    public class LLFireForgetBuilder : ILLBuilder
    {
        public LLFireForgetBuilder() { }

        public ILLRouter Build(AuthorizationHandlerContext authContext, string serviceConfigId)
        {
            var clientID = authContext.User.FindFirst("ClinetID")?.Value;
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
                    OutputRequired = (bool)setup.GetValue("OutputRequired"),
                    Action = setup.GetValue("Action").ToString(),
                    ArrayParameterTypes = setup.GetValue("ParameterDataTypes").ToObject<string[]>(),
                    ArrayParameterNames = setup.GetValue("ParameterNames").ToObject<string[]>(),
                    HttpMethod = setup.GetValue("HttpMethod").ToString()
                };

                fireForgetRouter.Steps[iter] = step;
                iter++;
            }




            return fireForgetRouter;
        }

        

    }
}

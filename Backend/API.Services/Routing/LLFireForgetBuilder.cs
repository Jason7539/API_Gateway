using API.Models.gateway;
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

        public ILLRouter Build(string serviceConfigId)
        {
            var configData;
            using (var context = new ApiGatewayContext())
            {
                configData = context.Configuration.Where(con => con.EndPoint.Equals(serviceConfigId));
            }

            var array = JArray.Parse(configData);
            var fireForgetRouter = new LLFireForgetRouterAsync(array.Count());
            var iter = 0;
            foreach(JObject setup in array)
            {
                IStep step = new Step();
                step.Async=setup.GetValue("Async");
            }
            



        }

        

    }
}

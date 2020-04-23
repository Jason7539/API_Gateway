using API.Models.gateway;
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
            using (var context = new ApiGatewayContext())
            {
                var configData = context.Configuration.Where(con => con.EndPoint.Equals(serviceConfigId));
            }

                var fireForgetRouter = new LLFireForgetRouterAsync();

            



        }

        

    }
}

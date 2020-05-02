using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Protocols.WSIdentity;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace API.Managers
{
    /// <summary>
    /// Class for Managing Different Router Calls
    /// </summary>
    public class RoutingManager
    {
        public RoutingManager() { }


        public void RouteExecute(AuthorizationHandlerContext authContext, string serviceConfigID, string callbackUrl)
        {
            var urlValidation = new UrlValidationService();
            var builder = new LLFireForgetBuilder();
            ILLRouter router;
            try
            {
                router = builder.Build(authContext, serviceConfigID);
                router.Execute();
               
            }
            catch (UriFormatException e)
            {
                
            }
            catch (InvalidInputException e)
            {

            }
            catch (Exception e)
            {

            }
            


        }


        //private HttpRequestMessage CraftHttpRequestMessage(string methodType, string action)
        //{
        //    HttpRequestMessage message;
        //    switch (methodType){
        //        case "HEAD":
        //            message = new HttpRequestMessage(HttpMethod.Head, action);
        //            break;
        //        case "GET":
        //            message = new HttpRequestMessage(HttpMethod.Get, action);
        //            break;
        //        case "POST":
        //            message = new HttpRequestMessage(HttpMethod.Post, action);
        //            break;
        //        case "PUT":
        //            message = new HttpRequestMessage(HttpMethod.Put, action);
        //            break;
        //        case "DELETE":
        //            message = new HttpRequestMessage(HttpMethod.Delete, action);
        //            break;
        //        case "OPTIONS":
        //            message = new HttpRequestMessage(HttpMethod.Options, action);
        //            break;
        //        case "TRACE":
        //            message = new HttpRequestMessage(HttpMethod.Trace, action);
        //            break;
        //        default:
        //            throw new InvalidInputException("MethodType was not a proper type");
        //    }

        //    return message;
        //}

    }
}

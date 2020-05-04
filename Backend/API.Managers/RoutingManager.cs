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
        ILLBuilder Builder { get; set; }
        JWTService AuthService { get; set; }
        public RoutingManager(JWTService authService, ILLBuilder builder) 
        {
            AuthService = AuthService;
            Builder = builder;
        }

        //change to use authService and accept the http request object
        public HttpResponseMessage RouteExecute(String authContext, string serviceConfigID, string callbackUrl)
        {
            HttpResponseMessage message;
            ILLRouter router;
            try
            {
                if (AuthService.ValidateHmacSignedJWTToken(authContext))
                {
                    router = Builder.Build(authContext, serviceConfigID);
                    router.Execute();
                }
            }
            catch (UriFormatException e)
            {
                throw;
            }
            catch (InvalidCastException e)
            {
                throw;
            }
            catch(InvalidInputException e)
            {

            }
            catch (Exception e)
            {
                throw;
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

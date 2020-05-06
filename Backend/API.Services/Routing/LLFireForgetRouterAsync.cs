using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Web.Mvc;

namespace API.Services
{
    public class LLFireForgetRouterAsync:ILLRouter
    {
        public IStep [] Steps { get; set; }
        public int ReturnStep { get; set; }
        //public string CallbackUrl { get; set; }
        public HttpRequest InitialRequest { get; set; }

        public LLFireForgetRouterAsync(int stepCount)
        {
            Steps = new IStep[stepCount];
        }
        public LLFireForgetRouterAsync(int stepCount, int returnStep) 
        {
            Steps = new IStep[stepCount];
            ReturnStep = returnStep;
        }

        /// <summary>
        /// Currently only can handle array of 1 step, but future implementations plan to hold the ability to handle multiple steps
        /// returns string received from the Returning Step
        /// </summary>
        /// <returns></returns>
        public string Execute()
        {
            //turns initial request from controller into httpContent so it can be processed by the executeStep(httpcontent message) 
            var content = CreateContent();
            for (int i = 0; i < Steps.Length; i++)
            {
                content = Steps[i].ExecuteStep(content);
                if(i==ReturnStep)
                {
                    return content;
                }
            }

            return null;
        }

        private string CreateContent()
        {
            using (var bodyReader = new StreamReader(InitialRequest.Body))
            {
                var bodyStr = bodyReader.ReadToEnd();
                return bodyStr;
            }
            //return null;
        }
    }
}

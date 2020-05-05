using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace API.Services
{
    public class LLFireForgetRouterAsync:ILLRouter
    {
        public IStep [] Steps { get; set; }
        public int ReturnStep { get; set; }
        public string CallbackUrl { get; set; }
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

        public HttpResponseMessage Execute()
        {
            //HttpResponseMessage message;
            for(int i = 0; i < Steps.Length; i++)
            {





            }


        }
    }
}

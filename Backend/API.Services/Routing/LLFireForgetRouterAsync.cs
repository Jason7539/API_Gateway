using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Services
{
    public class LLFireForgetRouterAsync:ILLRouter
    {
        public IStep [] Steps { get; set; }
        public int ReturnStep { get; set; }
        public string CallbackUrl { get; set; }

        public LLFireForgetRouterAsync(int stepCount)
        {
            Steps = new IStep[stepCount];
        }
        public LLFireForgetRouterAsync(int stepCount, int returnStep) 
        {
            Steps = new IStep[stepCount];
            ReturnStep = returnStep;
        }

        public void Execute()
        {



            
            for(int i = 0; i < Steps.Length; i++)
            {





            }


        }
    }
}

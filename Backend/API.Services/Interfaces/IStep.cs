using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Web.Mvc;

namespace API.Services
{
    /// <summary>
    /// IStep is an interface that can be inherited from to ensure that the chain of execution is correct
    /// the IStep passes along a context object in order to pass data down
    /// </summary>
    public interface IStep
    {
        //IStep Next{get;set;}
        bool Async { get; set; }
        string Action { get; set; }
        string HttpMethod { get; set; }
        //bool OutputRequired { get; set; }
        string[] ArrayParameterTypes { get; set; }
        string[] ArrayParameterNames { get; set; }
        HttpRequestMessage Message { get; set; }
        string ExecuteStep(string pastResult);

    }
}

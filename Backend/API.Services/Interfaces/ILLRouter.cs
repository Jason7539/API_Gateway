using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Web.Mvc;

namespace API.Services
{
    /// <summary>
    /// interface for different routers that can be bult and executed
    /// </summary>
    public interface ILLRouter
    {
        IStep[] Steps { get; set; }
        int ReturnStep { get; set; }
        //string CallbackUrl { get; set; }
        string Execute();
    }
}

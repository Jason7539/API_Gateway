using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Services
{
    /// <summary>
    /// Interface for different builders on api Routing Builder
    /// </summary>
    public interface ILLBuilder
    {
        ILLRouter Build(HttpRequest initialRequest, string authContext);

    }
}

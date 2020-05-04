using Microsoft.AspNetCore.Authorization;
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
        ILLRouter Build(String authContext, string serviceConfigData);

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace API.Services
{
    /// <summary>
    /// interface for different routers that can be bult and executed
    /// </summary>
    public interface ILLRouter
    {
        void Execute();
    }
}

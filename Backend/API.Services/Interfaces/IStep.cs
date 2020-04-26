using System;
using System.Collections.Generic;
using System.Text;

namespace API.Services
{
    /// <summary>
    /// IStep is an interface that can be inherited from to ensure that the chain of execution is correct
    /// the IStep passes along a context object in order to pass data down
    /// </summary>
    public interface IStep
    {
        IStep Next{get;set;}
        void SetNext(IStep step);
        void Execute();
        
    }
}

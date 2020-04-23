using System;
using System.Collections.Generic;
using System.Text;

namespace API.Services
{
    public class LLFireForgetRouterAsync
    {
        public RouterContext Context { get; set; }
        public IStep Head
        {
            get
            {
                return Head;
            }
            private set
            {
                if(Head == null)
                {
                    Head = value;
                }
                else
                {
                    Head.SetNext(value);
                }
            }
        }

        public LLFireForgetRouterAsync() { }


    }
}

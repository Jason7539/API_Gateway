using System;
using System.Collections.Generic;
using System.Text;

namespace API.Services
{
    class Step : IStep
    {
        public IStep Next { get; set; }

        public Step()
        { }
        public void SetNext(IStep next)
        {
            if (Next == null)
            {
                Next = next;
            }
            else
            {
                Next.SetNext(next);
            }
        }

        public void Execute(RouterContext context)
        {

        }
    }
}

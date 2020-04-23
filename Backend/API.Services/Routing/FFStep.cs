using System;
using System.Collections.Generic;
using System.Text;

namespace API.Services
{
    public class FFStep: IStep
    {
        public IStep Next { get; set; }
        public FFStep()
        {

        }

        public void SetNext(IStep next)
        {
            if(Next==null)
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

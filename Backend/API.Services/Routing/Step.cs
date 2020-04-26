using System;
using System.Collections.Generic;
using System.Text;

namespace API.Services
{
    class Step : IStep
    {
        public IStep Next { get; set; }
        public bool Async { get; set; }
        public string Action { get; set; }
        public bool OutputRequired { get; set; }
        public string[] ArrayParameterTypes { get; set; }
        public string[] ArrayParameterNames { get; set; }


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

        public void Execute()
        {

        }
    }
}

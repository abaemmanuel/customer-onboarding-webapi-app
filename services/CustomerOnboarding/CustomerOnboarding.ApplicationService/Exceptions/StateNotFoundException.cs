using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnboarding.ApplicationService.Exceptions
{
    public class StateNotFoundException : Exception
    {
        public StateNotFoundException()
        {
        }

        public StateNotFoundException(string message)
            : base(message)
        {
        }

        public StateNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

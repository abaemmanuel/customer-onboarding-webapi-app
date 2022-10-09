using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnboarding.ApplicationService.Exceptions
{
    public class OnboardCustomerException : Exception
    {
        public OnboardCustomerException()
        {
        }

        public OnboardCustomerException(string message)
            : base(message)
        {
        }

        public OnboardCustomerException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

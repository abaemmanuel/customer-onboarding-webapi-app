using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnboarding.ApplicationService.Services.Exceptions
{
    public class OtpNotSentException : Exception
    {
        public OtpNotSentException()
        {
        }

        public OtpNotSentException(string message)
            : base(message)
        {
        }

        public OtpNotSentException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

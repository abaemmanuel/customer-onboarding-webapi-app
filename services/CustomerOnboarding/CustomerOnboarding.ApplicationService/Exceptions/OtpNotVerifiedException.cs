using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnboarding.ApplicationService.Services.Exceptions
{

    public class OtpNotVerifiedException : Exception
    {
        public OtpNotVerifiedException()
        {
        }

        public OtpNotVerifiedException(string message)
            : base(message)
        {
        }

        public OtpNotVerifiedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

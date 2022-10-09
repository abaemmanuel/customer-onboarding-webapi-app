using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnboarding.ApplicationService.Services.Interfaces
{
    public interface IOtpService
    {
        Task<bool> SendOTP(string phoneNumber);
        Task<bool> VerifiedOTP(string phoneNumber);
    }
}

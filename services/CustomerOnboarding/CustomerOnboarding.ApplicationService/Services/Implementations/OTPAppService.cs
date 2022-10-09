using CustomerOnboarding.ApplicationService.Services.Exceptions;
using CustomerOnboarding.ApplicationService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnboarding.ApplicationService.Services.Implementations
{
    public class OTPAppService : IOtpService
    {
        public async Task<bool> SendOTP(string phoneNumber)
        {
            var otpSent = true;
            if (otpSent == false)
            {
                throw new OtpNotSentException($"OTP could not be sent to {phoneNumber},pls try again later.");
            }
            return otpSent;
        }

        public async Task<bool> VerifiedOTP(string phoneNumber)
        {
            var otpVerified = true;
            if (otpVerified == false)
            {
                throw new OtpNotVerifiedException($"OTP could not be verified for {phoneNumber},pls try again later.");
            }
            return otpVerified;
        }
    }
}

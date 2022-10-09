using CustomerOnboarding.ApplicationService.Services.Implementations;
using CustomerOnboarding.ApplicationService.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnboarding.ApplicationService.ExtentionMethods
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddCustomerOnboardingApplicationService(
            this IServiceCollection services)
        {
            services.AddTransient<ICustomerOnboarder,OnboardCustomerAppService>();
            services.AddTransient<IOtpService,OTPAppService>();
            services.AddTransient<IPasswordHasher,PasswordHasher>();
            services.AddTransient<IStateAppService, StateAppService>();
            services.AddTransient<ILgaAppService, LgaAppService>();
            services.AddTransient<IOnboardingStatusAppService, OnboardingStatusAppService>();
            services.AddTransient<IGetBankService, GetBankServices>();


            return services;
        }
    }
}

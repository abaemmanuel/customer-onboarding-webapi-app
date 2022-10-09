using CustomerOnboarding.Repositories.Repositories;
using CustomerOnboarding.Repositories.Repository;
using CustomerOnboarding.Repository.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnboarding.Repository.ExtentionMethods
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomerOnboardingRepositoryServices(
            this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<CustomerOnboardingContext>(options =>            
            options.UseSqlServer(configuration.GetConnectionString("CustomerOnboardingConnectionString")));
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

            return services;
        }
    }
}

using CustomerOnboarding.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CustomerOnboarding.Repository.DbContexts
{
    public class CustomerOnboardingContext : DbContext
    {
        public CustomerOnboardingContext(DbContextOptions<CustomerOnboardingContext> option)
            : base(option)
        {
        }
        public DbSet<State> States { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<LocalGovernmentArea> LocalGovernmentAreas { get; set; }
        public DbSet<OnboardingStatus> OnboardingStatus { get; set; }

    }
}

using CustomerOnboarding.ApplicationService.Dtos;
using CustomerOnboarding.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnboarding.ApplicationService.Services.Interfaces
{
    public interface ICustomerOnboarder
    {
        Task<bool> OnboardCustomer(CustomerDto customer);
        Task<IEnumerable<Customer>> GetAllOnboardedCustomers();

        Task<Customer> GetOnboardedCustomerById(long customerId);
        Task<bool> UpdateOnboardedCustomer(Customer customerToUpdate);
        Task<bool> DeleteOnboardedCustomer(Customer customerToDelet);
        Task<Customer> GetOnboardedCustomerByEmail(string customerEmail);
        Task<Customer> GetOnboardedCustomerByPhoneNumber(string customerPhoneNumber);
    }
}

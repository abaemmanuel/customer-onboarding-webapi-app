using CustomerOnboarding.ApplicationService.Dtos;
using CustomerOnboarding.ApplicationService.Exceptions;
using CustomerOnboarding.ApplicationService.Services.Exceptions;
using CustomerOnboarding.ApplicationService.Services.Interfaces;
using CustomerOnboarding.Core.Entities;
using CustomerOnboarding.Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnboarding.ApplicationService.Services.Implementations
{
    public class OnboardCustomerAppService : ICustomerOnboarder
    {
        private readonly IOtpService _otpService;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<OnboardingStatus> _OnboardingStatusRepository;
        private readonly IRepository<LocalGovernmentArea> _lgaRepository;
        private readonly IRepository<State> _stateRepository;
        private readonly IPasswordHasher _passwordHasher;

        public OnboardCustomerAppService(IOtpService otpService,
            IRepository<Customer> customerRepository,
            IRepository<OnboardingStatus> OnboardingStatusRepository,
            IRepository<LocalGovernmentArea> lgaRepository,
            IRepository<State> stateRepository,
            IPasswordHasher passwordHasher)
        {
            _otpService = otpService;
            _customerRepository = customerRepository;
            _OnboardingStatusRepository = OnboardingStatusRepository;
            _lgaRepository = lgaRepository;
            _stateRepository = stateRepository;
            _passwordHasher = passwordHasher;
        }
        public async Task<bool> OnboardCustomer(CustomerDto customer) 
        {
            var previouslyOnboardedCustomerWithSameEmail = await _customerRepository
                .GetByWhere(x => x.Email == customer.Email);
            
            var existingCustomer = previouslyOnboardedCustomerWithSameEmail.SingleOrDefault();

            if (existingCustomer != null)
            {
                throw new OnboardCustomerException($"Customer with email {customer.Email} has already been onboarded");
            }

            var otpIsSent = await _otpService.SendOTP(customer.PhoneNumber);
            var otpIsVerified = await _otpService.VerifiedOTP(customer.PhoneNumber);

            var getStateByNameResult = await _stateRepository
                .GetByWhere(x => x.Name == customer.StateOfResidence);

            var getStateByName = getStateByNameResult.SingleOrDefault();

            if (getStateByName == null)
            {
                throw new StateNotFoundException($"Error onboarding customer as state with name {customer.StateOfResidence} was not found");
            }

            long stateId = getStateByName.Id;

            var lgasMappedToState = await _lgaRepository
                .GetByWhere(x => x.StateId == stateId && x.Lga == customer.LGA);

            var lgaIsMappedToState = lgasMappedToState.Any();

            if (otpIsSent && otpIsVerified && lgaIsMappedToState)
            {
                var newCustomer = new Customer
                {
                    PhoneNumber = customer.PhoneNumber,
                    DateOnboarded = DateTime.Now,
                    Email = customer.Email,
                    OnboardingStatusId = await GetOnboardingStatusId("Completed"),
                    Password = _passwordHasher.HashPassword(customer.Password)
                };
                await _customerRepository.CreateAsync(newCustomer);
                return true;
             }

            throw new OnboardCustomerException($"Local government area ({customer.LGA}) which was provided " +
                $"can not be mapped to the state of residence ({customer.StateOfResidence})");
        }

        public async Task<IEnumerable<Customer>> GetAllOnboardedCustomers()
        {
            var completedOnboardedStatusId = await GetOnboardingStatusId("Completed");

            var allOnboardedCustomers = await _customerRepository
                .GetByWhere(x => x.OnboardingStatusId == completedOnboardedStatusId);

            return allOnboardedCustomers;
        }
        private async Task<long> GetOnboardingStatusId(string statusDescription)
        {
            long newOnboardingStatusId = 0;
            long pendingOnboardingStatusId = 0;
            var allOnboardingStatus = await _OnboardingStatusRepository.GetAll();

            foreach (var status in allOnboardingStatus)
            {
                if (status.Description == statusDescription)
                {
                    newOnboardingStatusId = status.Id;
                }
                if (status.Description == "Pending")
                {
                    pendingOnboardingStatusId = status.Id;
                }
            }

            return newOnboardingStatusId == 0 ? pendingOnboardingStatusId : newOnboardingStatusId;
        }

        public async Task<Customer> GetOnboardedCustomerById(long customerId)
        {
            var completedOnboardedStatusId = await GetOnboardingStatusId("Completed");

            var onboardedCustomer = await _customerRepository
                        .GetByWhere(x => x.Id == customerId &&
                        x.OnboardingStatusId == completedOnboardedStatusId);

            return onboardedCustomer.SingleOrDefault();
        }

        public async Task<bool> UpdateOnboardedCustomer(Customer customerToUpdate)
        {
            await _customerRepository.UpdateAsync(customerToUpdate);
            return true;
        }

        public async Task<bool> DeleteOnboardedCustomer(Customer customerToDelete)
        {
            await _customerRepository.DeleteAsync(customerToDelete);
            return true;
        }

        public async Task<Customer> GetOnboardedCustomerByEmail(string customerEmail)
        {
           var customerByEmail =  await _customerRepository
                .GetByWhere(x => x.Email.ToLower() == customerEmail.ToLower());

           return customerByEmail.SingleOrDefault();
        }

        public async Task<Customer> GetOnboardedCustomerByPhoneNumber(string customerPhoneNumber)
        {
            var customerByPhoneNumber = await _customerRepository
                .GetByWhere(x => x.PhoneNumber.ToLower() == customerPhoneNumber.ToLower());

            return customerByPhoneNumber.SingleOrDefault();
        }

    }
}

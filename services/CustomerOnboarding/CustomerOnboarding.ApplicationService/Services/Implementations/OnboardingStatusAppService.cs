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
    public class OnboardingStatusAppService : IOnboardingStatusAppService
    {
        private readonly IRepository<OnboardingStatus> _onboardingStatusRepository;

        public OnboardingStatusAppService(IRepository<OnboardingStatus> onboardingStatusRepository)
        {
            _onboardingStatusRepository = onboardingStatusRepository;
        }
        public async Task<bool> AddOnboardingStatus(OnboardingStatus onboardingStatusToAdd)
        {
            await _onboardingStatusRepository.CreateAsync(onboardingStatusToAdd);
            return true;
        }

        public async Task<bool> DeleteOnboardingStatus(OnboardingStatus onboardingStatusToDelete)
        {
            await _onboardingStatusRepository.DeleteAsync(onboardingStatusToDelete);
            return true;
        }

        public async Task<OnboardingStatus> GetOnboardingStatusByDescription(string OnboardingStatusDescription)
        {
            var result = await _onboardingStatusRepository
                .GetByWhere(x => x.Description.ToLower() == OnboardingStatusDescription.ToLower());
            return result.SingleOrDefault();
        }

        public async Task<OnboardingStatus> GetOnboardingStatusById(long OnboardingStatusId)
        {
            var result = await _onboardingStatusRepository
                .GetByWhere(x => x.Id == OnboardingStatusId);
            return result.SingleOrDefault();
        }

        public async Task<bool> UpdateOnboardingStatus(OnboardingStatus onboardingStatusToUpdate)
        {
            await _onboardingStatusRepository.UpdateAsync(onboardingStatusToUpdate);
            return true;
        }
    }
}

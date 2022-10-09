using CustomerOnboarding.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnboarding.ApplicationService.Services.Interfaces
{
    public interface IOnboardingStatusAppService
    {
        Task<OnboardingStatus> GetOnboardingStatusById(long OnboardingStatusId);
        Task<OnboardingStatus> GetOnboardingStatusByDescription(string OnboardingStatusDescription);
        Task<bool> UpdateOnboardingStatus(OnboardingStatus OnboardingStatusToUpdate);
        Task<bool> DeleteOnboardingStatus(OnboardingStatus OnboardingStatusToDelete);
        Task<bool> AddOnboardingStatus(OnboardingStatus OnboardingStatusToAdd);
    }
}

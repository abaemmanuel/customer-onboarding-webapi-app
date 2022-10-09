using CustomerOnboarding.Core.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerOnboarding.ApplicationService.Dtos;

namespace CustomerOnboarding.ApplicationService.Services.Interfaces
{
    public interface IGetBankService
    {
        Task<Response<ListGetbankDto>> GetbankRequest();
    }
}

using CustomerOnboarding.ApplicationService.Dtos;
using CustomerOnboarding.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnboarding.ApplicationService.Services.Interfaces
{
    public interface ILgaAppService
    {
        Task<LgasDto> GetLgaById(long lgaId);
        Task<IEnumerable<LgasDto>> GetByStateId(long stateId);
        Task<LgasDto> GetLgaByLgaName(string lgaName);
        Task<IEnumerable<LgasDto>> GetAllLgas();
        Task<bool> DeleteLga(long lgaId);
        Task<bool> UpdatLga(UpdateLgaDto lgaToUpdate);
        Task<LgasDto> AddLga(AddLgaDto lgaToBeAdded);
    }
}

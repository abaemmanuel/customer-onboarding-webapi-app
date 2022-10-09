using CustomerOnboarding.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnboarding.ApplicationService.Services.Interfaces
{
    public interface IStateAppService
    {
        Task<IEnumerable<State>> GetAll();
        Task<State> GetStateById(long stateId);
        Task<State> GetStateByName(string stateName);
        Task<bool> UpdateState(State stateToUpdate);
        Task<bool> DeleteState(State stateToDelete);
        Task<bool> AddState(State stateToAdd);
    }
}

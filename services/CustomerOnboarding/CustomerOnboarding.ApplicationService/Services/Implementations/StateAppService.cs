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
    public class StateAppService : IStateAppService
    {
        private readonly IRepository<State> _stateRepository;

        public StateAppService(IRepository<State> stateRepository)
        {
            _stateRepository = stateRepository;
        }
        public async Task<bool> AddState(State stateToAdd)
        {
            await _stateRepository.CreateAsync(stateToAdd);
            return true;
        }

        public async Task<bool> DeleteState(State stateToDelete)
        {
            await _stateRepository.DeleteAsync(stateToDelete);
            return true;
        }

        public async Task<IEnumerable<State>> GetAll()
        {
            var result = await _stateRepository.GetAll();
            return result.AsEnumerable();
        }

        public async Task<State> GetStateById(long stateId)
        {
            var result =  await _stateRepository.GetByWhere(x => x.Id == stateId);
            return result.SingleOrDefault();
        }

        public async Task<State> GetStateByName(string stateName)
        {
            var result = await _stateRepository
                .GetByWhere(x => x.Name.ToLower() == stateName.ToLower());
            return result.SingleOrDefault();
        }

        public async Task<bool> UpdateState(State stateToUpdate)
        {
            await _stateRepository.UpdateAsync(stateToUpdate);
            return true;
        }

    }
}

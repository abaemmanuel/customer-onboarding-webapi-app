using CustomerOnboarding.ApplicationService.Dtos;
using CustomerOnboarding.ApplicationService.Services.Interfaces;
using CustomerOnboarding.Core.Dto;
using CustomerOnboarding.Core.Entities;
using CustomerOnboarding.Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnboarding.ApplicationService.Services.Implementations
{
    public class LgaAppService : ILgaAppService
    {
        private readonly IRepository<LocalGovernmentArea> _lgaRepository;
        private readonly IRepository<State>_stateRepository;

        public LgaAppService(IRepository<LocalGovernmentArea> lgaRepository,
            IRepository<State> stateRepository)
        {
            _lgaRepository = lgaRepository;
            _stateRepository = stateRepository;
        }


        public async Task<LgasDto> AddLga(AddLgaDto lgaToBeAdded)
        {
            var existingState = await _stateRepository.GetByWhere(x => x.Name == lgaToBeAdded.State);
            var state = existingState.SingleOrDefault();

            if (state == null)
            {
                throw new Exception($"Local government area {lgaToBeAdded.LGA} can not be created because state {lgaToBeAdded.State} was not found");
            }

            var mappedLgaTobeAdded = new LocalGovernmentArea { Lga = lgaToBeAdded.LGA, StateId = state.Id };
            await _lgaRepository.CreateAsync(mappedLgaTobeAdded);

            return new LgasDto
            {
                Id = mappedLgaTobeAdded.Id,
                LGA = mappedLgaTobeAdded.Lga,
                State = lgaToBeAdded.State
            };
        }

        public async Task<bool> DeleteLga(long lgaId)
        {
            var lga = await _lgaRepository.GetByWhere(x => x.Id == lgaId);

            var lgaTobeDeleted = lga.SingleOrDefault();

            if (lgaTobeDeleted == null) 
            {
                throw new Exception("Local government area with id {} can not be deleted because it was not found");

            }

            await _lgaRepository.DeleteAsync(lgaTobeDeleted);
            return true;
        }

        public async Task<LgasDto> GetLgaById(long lgaId)
        {
            var result = await _lgaRepository.GetByWhere(x => x.Id == lgaId);

            if (result.SingleOrDefault() == null)
            {
                throw new Exception("Local government area with id {lgaId} was not found");
            }

            return await MapLgaToLgaDto(result.SingleOrDefault());
        }
        public async Task<LgasDto> GetLgaByLgaName(string lgaName)
        {
            var result = await _lgaRepository.GetByWhere(x => x.Lga == lgaName);

            return await MapLgaToLgaDto(result.SingleOrDefault());
        }

        public async Task<IEnumerable<LgasDto>> GetByStateId(long stateId)
        {
            var result = await _lgaRepository
                .GetByWhere(x => x.Id == stateId);

            var lgasFromDb = result.AsEnumerable();

            List<LgasDto> lgasDto = new List<LgasDto>();

            foreach (var lga in lgasFromDb)
            {
                lgasDto.Add(await MapLgaToLgaDto(lga));
            }

            return lgasDto.AsEnumerable();
        }

        public async Task<bool> UpdatLga(UpdateLgaDto lgaToUpdated)
        {
            var lga = await _lgaRepository.GetByWhere(x => x.Id == lgaToUpdated.Id);

            var lgaTobeUpdated = lga.SingleOrDefault();

            if (lgaTobeUpdated == null)
            {
                throw new Exception("Local government area with id {} can not be updated because it was not found");

            }

            lgaTobeUpdated.Lga = lgaToUpdated.LGA;

            await _lgaRepository.UpdateAsync(lgaTobeUpdated);
            return true;
        }

        public async Task<IEnumerable<LgasDto>> GetAllLgas()
        {
            var result = await _lgaRepository.GetAll();

            var lgasFromDb = result.ToList();

            List<LgasDto> lgasDto = new List<LgasDto>();

            foreach (var lga in lgasFromDb)
            {
                lgasDto.Add(await MapLgaToLgaDto(lga));
            }

            return lgasDto.AsEnumerable();
        }

        private async Task<LgasDto> MapLgaToLgaDto(LocalGovernmentArea lga)
        {
            var stateNameResult = await _stateRepository
                        .GetByWhere(x => x.Id == lga.StateId);

            var stateName = stateNameResult.SingleOrDefault();

            if (stateName == null) { return null; }

            if (stateName.Name.ToLower() != "fct") { stateName.Name = stateName.Name + " State"; }

            var lgaDto = new LgasDto { Id = lga.Id, LGA = lga.Lga, State = stateName.Name };

            return lgaDto;
        }

    } 
}

using AutoMapper;
using CustomerOnboarding.ApplicationService.Dtos;
using CustomerOnboarding.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerOnboarding.Api.Mappers
{
    public class LgaProfiles : Profile
    {
        public LgaProfiles()
        {
            CreateMap<LocalGovernmentArea, LgasDto>();
        }
    }
}

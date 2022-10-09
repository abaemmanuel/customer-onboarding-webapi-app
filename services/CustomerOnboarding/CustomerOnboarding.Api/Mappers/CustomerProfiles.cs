using AutoMapper;
using CustomerOnboarding.ApplicationService.Dtos;
using CustomerOnboarding.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerOnboarding.Api.Mappers
{
    public class CustomerProfiles : Profile
    {
        public CustomerProfiles()
        {
            CreateMap<Customer, OnboardedCustomerDto>();
        }
    }
}

using CsvHelper.Configuration;
using CustomerOnboarding.Core.Dto;
using CustomerOnboarding.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerOnboarding.Api.Mappers
{
    public sealed class LgaCsvMap : ClassMap<LgaDto>
    {
        public LgaCsvMap()
        {
            Map(x => x.Lga).Name("Local Government Area");
            Map(x => x.StateCode).Name("StateCode");
            Map(x => x.StateName).Name("StateName");
        }
    }
}

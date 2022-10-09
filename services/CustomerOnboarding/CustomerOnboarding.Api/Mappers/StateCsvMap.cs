using CsvHelper.Configuration;
using CustomerOnboarding.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerOnboarding.Api.Mappers
{
    public sealed class StateCsvMap : ClassMap<State>
    {
        public StateCsvMap()
        {
            Map(x => x.Name).Name("State");
        }
    }    
}

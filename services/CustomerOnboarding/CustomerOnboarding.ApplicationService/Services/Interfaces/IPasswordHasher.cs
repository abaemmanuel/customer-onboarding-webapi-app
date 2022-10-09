using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnboarding.ApplicationService.Services.Interfaces
{
    public interface IPasswordHasher
    {
        string HashPassword(string plainTextPassword);
        bool MatchPassword(string passwordToMatch, string hashedPassword);
    }
}

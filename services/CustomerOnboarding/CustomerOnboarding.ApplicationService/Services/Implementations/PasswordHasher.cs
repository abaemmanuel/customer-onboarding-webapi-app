using CustomerOnboarding.ApplicationService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnboarding.ApplicationService.Services.Implementations
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string plainTextPassword)
        {
            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashed = BCrypt.Net.BCrypt.HashPassword(plainTextPassword, salt);

            return hashed;
        }

        public bool MatchPassword(string passwordToMatch, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(passwordToMatch, hashedPassword);
        }

    }
}

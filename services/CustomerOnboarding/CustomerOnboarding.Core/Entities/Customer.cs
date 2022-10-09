using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnboarding.Core.Entities
{
    public class Customer : Entity
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public long OnboardingStatusId { get; set; }
        public DateTime DateOnboarded { get; set; }
    }
}

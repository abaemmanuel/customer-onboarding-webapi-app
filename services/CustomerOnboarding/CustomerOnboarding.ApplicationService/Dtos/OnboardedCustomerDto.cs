using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnboarding.ApplicationService.Dtos
{
    public class OnboardedCustomerDto
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public long OnboardingStatusId { get; set; }
        public DateTime DateOnboarded { get; set; }
    }
}

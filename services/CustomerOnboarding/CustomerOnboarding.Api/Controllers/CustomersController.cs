using AutoMapper;
using CustomerOnboarding.ApplicationService.Dtos;
using CustomerOnboarding.ApplicationService.Exceptions;
using CustomerOnboarding.ApplicationService.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerOnboarding.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICustomerOnboarder _customerOnboarder;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(IMapper mapper,ICustomerOnboarder customerOnboarder,
            ILogger<CustomersController> logger)
        {
            _mapper = mapper;
            _customerOnboarder = customerOnboarder;
            _logger = logger;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> OnboardACustomer(CustomerDto newCustomer) 
        {
            try
            {
                var customerHasBeenOnboarded = await _customerOnboarder.OnboardCustomer(newCustomer);
                if (customerHasBeenOnboarded)
                {
                    return Ok("Customer onboarded Successfully.");
                }
                return StatusCode(500, "Customer onboarding process failed");
            }
            catch (OnboardCustomerException oex)
            {
                _logger.LogInformation(oex.Message);
                return BadRequest(oex.Message);
            }
            catch (StateNotFoundException stex)
            {
                _logger.LogInformation(stex.Message);
                return NotFound(stex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return StatusCode(500, "An error occurred, Try again later...");
            }
        }
        
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllOnboardedCustomers()
        {
            try
            {
                var onBoardedCustomers = await _customerOnboarder.GetAllOnboardedCustomers();
                if (onBoardedCustomers == null)
                {
                    return NotFound("Unable to retrieve details for onboarded customers");
                }

                List<OnboardedCustomerDto> mappedToOnboardedCustomerDto = 
                                            new List<OnboardedCustomerDto>();
                foreach (var customer in onBoardedCustomers)
                {
                    mappedToOnboardedCustomerDto.Add(_mapper.Map<OnboardedCustomerDto>(customer));
                }

                return Ok(mappedToOnboardedCustomerDto);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return StatusCode(500, "An error occurred, Try again later...");
            }
        }
    }
}

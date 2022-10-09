using CustomerOnboarding.ApplicationService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CustomerOnboarding.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetBankController : ControllerBase
    {
        private readonly IGetBankService _getBankServices;
        public GetBankController(IGetBankService getBankServices)
        {
            _getBankServices = getBankServices;
        }

        /// <summary>
        /// Get all BankName and BankCode
        /// </summary>
        /// <returns>Items in the Response List of Getbanks </returns>
        /// <remarks>
        /// 
        /// sample
        /// GET/api/GetBank
        /// </remarks> 
        /// <response code ="200">Get bank successfully</response>
        [HttpGet("Getbanks")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBanks()
        {
            var result = await _getBankServices.GetbankRequest();
            if (result.IsSuccessFul)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}

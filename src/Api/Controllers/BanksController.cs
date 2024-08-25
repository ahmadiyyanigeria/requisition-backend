using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{

    [Route("api/v{version:apiVersion}/banks")]
    [ApiController]
    public class BanksController : ControllerBase
    {
        private readonly IBankService _bankService;

        public BanksController(IBankService bankService)
        {
            _bankService = bankService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBanks()
        {
            var banks = await _bankService.GetBanksAsync();
            return Ok(banks);
           
        }

        [HttpGet("verify")]
        public async Task<IActionResult> VerifyBankAccount(string accountNumber, string bankCode)
        {
            var result = await _bankService.VerifyBankAccountAsync(accountNumber, bankCode);
            if (result.Status)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }
    }
}

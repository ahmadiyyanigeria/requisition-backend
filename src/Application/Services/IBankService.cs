using Application.DTOs;

namespace Application.Services
{
    public interface IBankService
    {
        Task<BankVerificationResponse> VerifyBankAccountAsync(string accountNumber, string bankCode);
        Task<List<BankDTO>> GetBanksAsync();
    }
}

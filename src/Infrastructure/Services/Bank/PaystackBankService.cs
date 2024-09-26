using Application.DTOs;
using Application.Services;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Net.Http.Headers;
using System.Runtime;

namespace Infrastructure.Services.Bank
{
    public class PaystackBankService : IBankService
    {
        private readonly HttpClient _httpClient;
        private readonly string _paystackSecretKey;

        public PaystackBankService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _paystackSecretKey = "Bearer sk_test_581011e8bdbb74260c9b6e172a1cf778e89f788f"; // Replace with your actual secret key

            // Set up the base URL and authentication headers
            _httpClient.BaseAddress = new System.Uri("https://api.paystack.co/");
            _httpClient.DefaultRequestHeaders.Add("Authorization", _paystackSecretKey);
        }

        public async Task<List<BankDTO>> GetBanksAsync()
        {
            var response = await _httpClient.GetAsync("bank");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var bankResponse = JsonConvert.DeserializeObject<BankResponse>(content);
                return bankResponse.Data;
            }

            throw new HttpRequestException($"Error retrieving banks: {response.StatusCode} - {response.ReasonPhrase}");
        }

        public async Task<BankVerificationResponse> VerifyBankAccountAsync(string accountNumber, string bankCode)
        {
            
            var endpoint = $"bank/resolve?account_number={accountNumber}&bank_code={bankCode}";
            var response = await _httpClient.GetAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<BankVerificationResponse>(content);
            }

            throw new HttpRequestException($"Invalid Bank Details:");
        }
    }
}

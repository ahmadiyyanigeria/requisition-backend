namespace Domain.Entities.Common
{
    public class BankAccount(string accountNumber, string bankName, string accountName, string iban, string swift)
    {
        public string AccountNumber { get; private set; } = accountNumber;
        public string BankName { get; private set; } = bankName;
        public string AccountName { get; private set; } = accountName;
        public string IBAN { get; private set; } = iban;
        public string SWIFT { get; private set; } = swift;
    }
}

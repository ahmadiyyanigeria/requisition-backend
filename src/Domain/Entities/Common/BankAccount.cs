namespace Domain.Entities.Common
{
    public class BankAccount
    {
        public string AccountNumber { get; private set; }
        public string BankName { get; private set; }
        public string AccountName { get; private set; }
        public string IBAN { get; private set; }
        public string SWIFT { get; private set; }

        private BankAccount() { }
        public BankAccount(string accountNumber, string bankName, string accountName, string iban, string swift)
        {
            AccountNumber = accountNumber;
            BankName = bankName;
            AccountName = accountName;
            IBAN = iban;
            SWIFT = swift;
        }
    }
}

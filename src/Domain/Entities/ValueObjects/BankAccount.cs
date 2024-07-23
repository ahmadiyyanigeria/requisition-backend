namespace Domain.Entities.ValueObjects
{
    public record BankAccount(
        string AccountNumber,
        string BankName,
        string AccountName,
        string IBAN,
        string SWIFT
    );
}

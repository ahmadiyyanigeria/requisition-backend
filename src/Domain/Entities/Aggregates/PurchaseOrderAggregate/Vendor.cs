using Domain.Entities.ValueObjects;

namespace Domain.Entities.Aggregates.PurchaseOrderAggregate
{
    public class Vendor
    {
        public Guid VendorId { get; private set; }
        public string Name { get; private set; } = default!;
        public string Address { get; private set; } = default!;
        public string ContactPerson { get; private set; } = default!;
        public string? ContactEmail { get; private set; }
        public string ContactPhone { get; private set; } = default!;
        //public BankAccount BankAccountDetails { get; private set; }

        private Vendor() { }
        public Vendor(string name, string address, string contactPerson, string contactEmail, string contactPhone)
        {
            VendorId = Guid.NewGuid();
            Name = name;
            Address = address;
            ContactPerson = contactPerson;
            ContactEmail = contactEmail;
            ContactPhone = contactPhone;
            //BankAccountDetails = bankAccountDetails;
        }
    }
}

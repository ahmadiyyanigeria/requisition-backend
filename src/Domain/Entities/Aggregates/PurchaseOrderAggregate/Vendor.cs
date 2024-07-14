using Domain.Entities.Common;

namespace Domain.Entities.Aggregates.PurchaseOrderAggregate
{
    public class Vendor
    {
        public Guid VendorId { get; private set; }
        public string Name { get; private set; }
        public string Address { get; private set; }
        public string ContactPerson { get; private set; }
        public string ContactEmail { get; private set; }
        public string ContactPhone { get; private set; }
        public BankAccount BankAccountDetails { get; private set; }

        public Vendor(string name, string address, string contactPerson, string contactEmail, string contactPhone, BankAccount bankAccountDetails)
        {
            VendorId = Guid.NewGuid();
            Name = name;
            Address = address;
            ContactPerson = contactPerson;
            ContactEmail = contactEmail;
            ContactPhone = contactPhone;
            BankAccountDetails = bankAccountDetails;
        }
    }
}

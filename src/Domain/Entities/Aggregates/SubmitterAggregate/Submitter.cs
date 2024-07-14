using Domain.Entities.Aggregates.RequisitionAggregate;
using Domain.Entities.Common;

namespace Domain.Entities.Aggregates.SubmitterAggregate
{
    public class Submitter
    {
        public Guid SubmitterId { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Position { get; private set; }
        public string Department { get; private set; }
        public BankAccount BankAccount { get; private set; }
        public IReadOnlyList<Requisition> Requisitions { get; private set; } = [];

        private Submitter() { }
        public Submitter(string name, string email, string position, string department, BankAccount bankAccount)
        {
            Name = name;
            Email = email;
            Position = position;
            Department = department;
            BankAccount = bankAccount;
        }
    }
}

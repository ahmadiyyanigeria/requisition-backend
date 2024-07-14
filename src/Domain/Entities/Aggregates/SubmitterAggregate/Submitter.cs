using Domain.Entities.Aggregates.RequisitionAggregate;
using Domain.Entities.Common;

namespace Domain.Entities.Aggregates.SubmitterAggregate
{
    public class Submitter(string name, string email, string position, string department, BankAccount bankAccount)
    {
        public Guid SubmitterId { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; } = name;
        public string Email { get; private set; } = email;
        public string Position { get; private set; } = position;
        public string Department { get; private set; } = department;
        public BankAccount BankAccount { get; private set; } = bankAccount;
        public IReadOnlyList<Requisition> Requisitions { get; private set; } = [];
    }
}

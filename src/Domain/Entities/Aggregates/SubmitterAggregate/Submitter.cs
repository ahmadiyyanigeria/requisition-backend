using Domain.Entities.Aggregates.RequisitionAggregate;
using Domain.Entities.ValueObjects;

namespace Domain.Entities.Aggregates.SubmitterAggregate
{
    public class Submitter
    {
        public Guid SubmitterId { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; } = default!;
        public string UserId { get; private set; } = default!;
        public string? Email { get; private set; }
        public string Position { get; private set; } = default!;
        public string Department { get; private set; } = default!;

        private Submitter() { }
        public Submitter(string userId, string name, string? email, string position, string department)
        {
            UserId = userId;
            Name = name;
            Email = email;
            Position = position;
            Department = department;
        }
    }
}

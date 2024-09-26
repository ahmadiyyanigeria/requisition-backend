using Domain.Entities.Aggregates.RequisitionAggregate;
using Domain.Entities.ValueObjects;
using Domain.Enums;

namespace Domain.Factories
{
    public interface IRequisitionFactory
    {
        Task<Requisition> Create(Guid submitterId, string description, string expenseHead, RequisitionType requisitionType, BankAccount? bankAccount, string department);
    }
}

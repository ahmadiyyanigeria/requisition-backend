using Domain.Entities.Aggregates.RequisitionAggregate;
using Domain.Entities.ValueObjects;
using Domain.Enums;
using Domain.Repositories;

namespace Domain.Factories
{
    public class RequisitionFactory : IRequisitionFactory
    {
        private readonly IRequisitionRepository _requisitionRepository;

        public RequisitionFactory(IRequisitionRepository requisitionRepository)
        {
            _requisitionRepository = requisitionRepository;
        }

        public async Task<Requisition> Create(Guid submitterId, string description, string expenseHead, RequisitionType requisitionType, BankAccount? bankAccount, string department)
        {
            string requisitionNumber;
            do
            {
                requisitionNumber = $"REQ-{DateTime.UtcNow:yyyyMMddHHmmssfff}";
            }
            while (await _requisitionRepository.RequisitionNumberExistsAsync(requisitionNumber));

            return new Requisition(submitterId, description, expenseHead, requisitionType, bankAccount, department, requisitionNumber);
        }
    }
}

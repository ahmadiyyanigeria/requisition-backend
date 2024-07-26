using MediatR;
using Application.Repositories;
using Domain.Entities.Common;
using Domain.Exceptions;

namespace Application.Commands
{
    public class CreateExpenseHead
    {
        public class CreateExpenseHeadCommand : IRequest<string>
        {
            public string Name { get; init; } = default!;
            public string? Description { get; init; }
        }

        public class Handler : IRequestHandler<CreateExpenseHeadCommand, string>
        {
            private readonly IExpenseHeadRepository _expenseHeadRepository;
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IExpenseHeadRepository expenseHeadRepository, IUnitOfWork unitOfWork)
            {
                _expenseHeadRepository = expenseHeadRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<string> Handle(CreateExpenseHeadCommand request, CancellationToken cancellationToken)
            {
                var expenseHeadExist = await _expenseHeadRepository.ExistAsync(request.Name);
                if(expenseHeadExist)
                {
                    throw new DomainException("Expense head already exist", ExceptionCodes.ExpenseHeadAlreadyExist.ToString(), 400);
                }
                //creating the Expense Head object
                var expenseHead = new ExpenseHead(
                    request.Name,
                    request.Description);

                await _expenseHeadRepository.AddAsync(expenseHead);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return expenseHead.Name;
            }
        }
    }
}

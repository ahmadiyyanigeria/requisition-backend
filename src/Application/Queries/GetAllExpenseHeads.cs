using Application.Repositories;
using MediatR;

namespace Application.Queries
{
    public class GetAllExpenseHeads
    {
        public record Query : IRequest<List<string>>
        {
        }

        public class Handler : IRequestHandler<Query, List<string>>
        {
            private readonly IExpenseHeadRepository _expenseHeadRepository;
            public Handler(IExpenseHeadRepository expenseHeadRepository)
            {
                _expenseHeadRepository = expenseHeadRepository;
            }

            public async Task<List<string>> Handle(Query request, CancellationToken cancellationToken)
            {
                var expenseHeads = await _expenseHeadRepository.GetAllAsync();
                var retrievedExpensehead = expenseHeads.Select(n => n.Name).ToList();
                return retrievedExpensehead;
            }   
        }
    }
}

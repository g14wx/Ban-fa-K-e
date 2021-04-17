using System.Threading;
using System.Threading.Tasks;
using lab2.Domain.Contracts;
using MediatR;

namespace lab2.Application.Commands.ATM
{
    public class Credit
    {
        public record Command(int IdAccount,double amount) : IRequest<bool>;
        
        public class Handler : IRequestHandler<Command,bool>
        {
            private ICheckingAccountRepository _repository;

            public Handler(ICheckingAccountRepository repository)
            {
                _repository = repository;
            }
            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                bool response = await _repository.WithDrawMoney(request.IdAccount, request.amount);
                return response;
            }
        }
    }
}
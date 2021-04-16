using System.Threading;
using System.Threading.Tasks;
using lab2.Domain.Contracts;
using lab2.Domain.Models;
using MediatR;

namespace lab2.Application.Queries.SavingAccount
{
    public class GetSavingAccount
    {
        public record Query(int IdSavingAccount) : IRequest<CuentaAhorro>;
        
        public class Handler : IRequestHandler<Query, CuentaAhorro>
        {
            private ISavingAccountRepository _repository;
            public Handler(ISavingAccountRepository repository)
            {
                _repository = repository;
            }
            public async Task<CuentaAhorro> Handle(Query request, CancellationToken cancellationToken)
            {
                CuentaAhorro savingAccount = await _repository.FindSavingAccount(request.IdSavingAccount);
                return savingAccount;
            }
        }
    }
}
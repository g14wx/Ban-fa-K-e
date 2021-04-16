using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using lab2.Domain.Contracts;
using lab2.Domain.Models;
using MediatR;

namespace lab2.Application.Queries.SavingAccount
{
    public class GetAllSavingAccounts
    {
        public record Query(int IdCuentaBancaria) : IRequest<List<CuentaAhorro>>;
        
        public class Handler : IRequestHandler<Query, List<CuentaAhorro>>
        {
            private ISavingAccountRepository _repository;

            public Handler(ISavingAccountRepository repository)
            {
                _repository = repository;
            }
            public async Task<List<CuentaAhorro>> Handle(Query request, CancellationToken cancellationToken)
            {
                List<CuentaAhorro> ctnAhorros =await _repository.GetAllSavingAccountFromABankAccount(request.IdCuentaBancaria);
                return ctnAhorros != null ? ctnAhorros : null;
            }
        }
    }
}
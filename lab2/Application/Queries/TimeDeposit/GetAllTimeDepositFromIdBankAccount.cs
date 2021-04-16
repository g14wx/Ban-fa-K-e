using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using lab2.Domain.Contracts;
using lab2.Domain.Models;
using MediatR;

namespace lab2.Application.Queries.TimeDeposit
{
    public class GetAllTimeDepositFromIdBankAccount
    {
        public record Query(int IdCuentaBancaria) : IRequest<List<DepositoAPlazo>>;
        
        public class Handler : IRequestHandler<Query,List<DepositoAPlazo>>
        {
            private ITimeDepositRepository _repository;
            public Handler(ITimeDepositRepository repository)
            {
                _repository = repository;
            }
            public async Task<List<DepositoAPlazo>> Handle(Query request, CancellationToken cancellationToken)
            {
                List<DepositoAPlazo> dps =await _repository.getAllTimeDepositByIdBankAccount(request.IdCuentaBancaria);
                return dps;
            }
        }
    }
}
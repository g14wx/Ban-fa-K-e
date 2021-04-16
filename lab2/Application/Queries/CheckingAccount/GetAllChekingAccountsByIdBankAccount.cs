using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using lab2.Domain.Contracts;
using lab2.Domain.Models;
using MediatR;

namespace lab2.Application.Queries.CheckingAccount
{
    public class GetAllChekingAccountsByIdBankAccount
    {
        public record Query(int IdBankAccount) : IRequest<List<CuentaCorriente>>;
       
        public class Handler : IRequestHandler<Query, List<CuentaCorriente>>
        {
            private ICheckingAccountRepository _repository;
            public Handler(ICheckingAccountRepository repository)
            {
                _repository = repository;
            }
            public async Task<List<CuentaCorriente>> Handle(Query request, CancellationToken cancellationToken)
            {
                List<CuentaCorriente> ccs = await _repository.GetAllCheckingAccountByBankAccountId(request.IdBankAccount);
                return ccs;
            }
        }
    }
}
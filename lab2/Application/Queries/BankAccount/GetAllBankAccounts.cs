using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using lab2.Domain.Contracts;
using MediatR;

namespace lab2.Application.Queries.BankAccount
{
    public class GetAllBankAccounts
    {
        public record Query(int IdUser) : IRequest<List<Domain.Models.CuentaBancaria>>;
        
        
                public class Handler : IRequestHandler<Query, List<Domain.Models.CuentaBancaria>>
                {
                    private IBankAccountRepository _repository;
                    public Handler(IBankAccountRepository repo)
                    {
                        _repository = repo;
                    }
                    public async Task<List<Domain.Models.CuentaBancaria>> Handle(Query request, CancellationToken cancellationToken)
                    {
                       List<Domain.Models.CuentaBancaria> cuentaBancarias = await _repository.GetAllBankAccountFromAUser(request.IdUser);
                       return cuentaBancarias != null && cuentaBancarias.Count > 0 ? cuentaBancarias : null;
                    }
                } 
    }
}
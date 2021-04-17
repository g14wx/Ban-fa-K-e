using System;
using System.Threading;
using System.Threading.Tasks;
using EfModels.Models;
using lab2.Domain.Contracts;
using MediatR;
using CuentaBancaria = lab2.Domain.Models.CuentaBancaria;

namespace lab2.Application.Queries.ATM
{
    public class LoginQuery
    {
        public record Query(String NCuenta, String Pin) : IRequest<CuentaBancaria>;
        
        public class Handler : IRequestHandler<Query,CuentaBancaria>
        {
            private readonly IBankAccountRepository _repository;
            public Handler(IBankAccountRepository repository)
            {
                _repository = repository;
            }
            public async Task<CuentaBancaria> Handle(Query request, CancellationToken cancellationToken)
            {
                CuentaBancaria account = await _repository.FindBankAccountByNAccount(request.NCuenta, request.Pin);

                if (account != null)
                {
                    return account;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
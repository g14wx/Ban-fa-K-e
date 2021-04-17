using System;
using System.Threading;
using System.Threading.Tasks;
using CreditCardValidator;
using lab2.Domain.Contracts;
using MediatR;

namespace lab2.Application.Commands.BankAccount
{
    public class AddBankAccountCommand
    {
        
public record Command(String Pin, int IdUser) : IRequest<int>;

        public class Handler : IRequestHandler<Command, int>
        {
            private IBankAccountRepository _repository;
            private readonly Random _random;
            public Handler(IBankAccountRepository repository)
            {
                _repository = repository;

                _random = new Random();
        }

            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {

                Domain.Models.CuentaBancaria bankAccount = new Domain.Models.CuentaBancaria()
                {
                   IdUser = request.IdUser,
                    Pin = request.Pin,
                    NCuenta = CreditCardFactory.RandomCardNumber(CardIssuer.Visa,16)
                };
                bankAccount = await _repository.CreateAccountAsync(bankAccount);

                return bankAccount.Id;
            }
        }    
    }
}
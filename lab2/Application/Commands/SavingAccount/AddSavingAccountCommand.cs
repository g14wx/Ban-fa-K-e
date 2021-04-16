using System.Threading;
using System.Threading.Tasks;
using lab2.Domain.Contracts;
using MediatR;

namespace lab2.Application.Commands.SavingAccount
{
    public class AddSavingAccountCommand 
    {
        public record Command(double Saldo, double TasaInteresMensual, int IdCuentaBancaria): IRequest<int>;

        public class Handler : IRequestHandler<Command, int>
        {
            private ISavingAccountRepository _repository;

            public Handler(ISavingAccountRepository repository)
            {
                _repository = repository;
            }

            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                Domain.Models.CuentaAhorro savingAccount = new Domain.Models.CuentaAhorro()
                {
                    Saldo = request.Saldo,
                    IdCuentaBancaria = request.IdCuentaBancaria,
                    TasaInteresMensual = request.TasaInteresMensual
                };
                
                savingAccount = await _repository.CreateSavingAccount(savingAccount);

                return savingAccount.Id;
            }
        }
    }
}
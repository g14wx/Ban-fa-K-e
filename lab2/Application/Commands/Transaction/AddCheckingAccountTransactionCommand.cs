using System.Threading;
using System.Threading.Tasks;
using lab2.Domain.Contracts;
using System;
using MediatR;

namespace lab2.Application.Commands.Transaction
{
    public class AddCheckingAccountTransactionCommand
    {
        public record Command(int IdCuentaCorriente,double Cantidad, int Tipo, double Saldo, DateTime Fecha) : IRequest<bool>;
        public class Handler : IRequestHandler<Command, bool>
        {
            private ITransactionRepository _repository;

            public Handler(ITransactionRepository repository)
            {
                _repository = repository;
            }

            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                Domain.Models.Transaccion transaction = new Domain.Models.Transaccion()
                {
                    Cantidad = request.Cantidad,
                    IdCuentaCorriente = request.IdCuentaCorriente,
                    Tipo = request.Tipo,
                    Saldo = request.Saldo,
                    Fecha = request.Fecha
                };

                bool response = await _repository.RegisterTransaction(transaction);

                return response;
            }
        }
    }
}

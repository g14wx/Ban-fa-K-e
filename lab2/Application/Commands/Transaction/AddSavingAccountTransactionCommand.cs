using System.Threading;
using System.Threading.Tasks;
using lab2.Domain.Contracts;
using System;
using MediatR;
using System.Collections.Generic;

namespace lab2.Application.Commands.Transaction
{
    public class AddSavingAccountTransactionCommand
    {
        public record Command(int IdCuentaAhorro, double Cantidad, int Tipo, double Saldo, DateTime Fecha) : IRequest<Dictionary<string,string>>;
        public class Handler : IRequestHandler<Command, Dictionary<string, string>>
        {
            private ITransactionRepository _repository;

            public Handler(ITransactionRepository repository)
            {
                _repository = repository;
            }

            public async Task<Dictionary<string, string>> Handle(Command request, CancellationToken cancellationToken)
            {
                Domain.Models.Transaccion transaction = new Domain.Models.Transaccion()
                {
                    Cantidad = request.Cantidad,
                    IdCuentaAhorro = request.IdCuentaAhorro,
                    Tipo = request.Tipo,
                    Saldo = request.Saldo,
                    Fecha = request.Fecha
                };

                Dictionary<string, string> response = await _repository.RegisterTransaction(transaction);

                return response;
            }
        }
    }
}

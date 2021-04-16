using System;
using System.Threading;
using System.Threading.Tasks;
using lab2.Domain.Contracts;
using lab2.Domain.Models;
using MediatR;

namespace lab2.Application.Commands.TimeDeposit
{
    public class AddTimeDeposit
    {
        public record Command(double Cantidad, double TasaInteres, DateTime FechaPlazo, DateTime FechaInicio, int IdCuentaBancaria): IRequest<DepositoAPlazo>;
        
        public class Handler : IRequestHandler<Command, DepositoAPlazo>
        {
            private ITimeDepositRepository _repository;
            public Handler(ITimeDepositRepository repository)
            {
                _repository = repository;
            }
            public async Task<DepositoAPlazo> Handle(Command request, CancellationToken cancellationToken)
            {
                DepositoAPlazo dp = await _repository.CreateTimeDeposit(new DepositoAPlazo()
                {
                    Cantidad = request.Cantidad,
                    FechaInicio = request.FechaInicio,
                    FechaPlazo = request.FechaPlazo,
                    TasaInteres = request.TasaInteres,
                    IdCuentaBancaria = request.IdCuentaBancaria
                });

                return dp;
            }
        }
    }
}
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
        public record Command(double Cantidad, int CantidadDias, int IdCuentaBancaria): IRequest<DepositoAPlazo>;
        
        public class Handler : IRequestHandler<Command, DepositoAPlazo>
        {
            private ITimeDepositRepository _repository;
            public Handler(ITimeDepositRepository repository)
            {
                _repository = repository;
            }
            public async Task<DepositoAPlazo> Handle(Command request, CancellationToken cancellationToken)
            {
                double tasaDeInteres = GetInterestRateByNDays(request.CantidadDias);
                DepositoAPlazo dp = await _repository.CreateTimeDeposit(new DepositoAPlazo()
                {
                    Cantidad = request.Cantidad,
                    CantidadDias = request.CantidadDias,
                    TasaInteres = tasaDeInteres,
                    IdCuentaBancaria = request.IdCuentaBancaria
                });

                return dp;
            }

            public double GetInterestRateByNDays(int ndays)
            {
                if (ndays >= 30 && ndays <= 90)
                {
                    return 0.50;
                }else if (ndays>=120 && ndays <= 150)
                {
                    return 0.75;
                }else if (ndays>=180)
                {
                    return 1.00;
                }
                else
                {
                    return 0.0;
                }
            }
        }
    }
}
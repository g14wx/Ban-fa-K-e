using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using lab2.Domain.Contracts;
using lab2.Domain.DTOs;
using lab2.Domain.Models;
using MediatR;

namespace lab2.Application.Queries.SavingAccount
{
    public class GetAllSavingAccounts
    {
        public record Query(int IdCuentaBancaria) : IRequest<List<CuentaAhorroDTO>>;
        
        public class Handler : IRequestHandler<Query, List<CuentaAhorroDTO>>
        {
            private ISavingAccountRepository _repository;
            private IMapper _mapper;
            public Handler(ISavingAccountRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }
            public async Task<List<CuentaAhorroDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                List<CuentaAhorro> ctnAhorros =await _repository.GetAllSavingAccountFromABankAccount(request.IdCuentaBancaria);
                List<CuentaAhorroDTO> ctn = _mapper.Map<List<CuentaAhorro>, List<CuentaAhorroDTO>>(ctnAhorros);
                ctn = ctn.Select(dto =>
                    {
                        dto.TasaInteresMensual = GetInteres(dto.Saldo);
                        dto.Ganacias = CalcularGanancias(1, dto.TasaInteresMensual, dto.Saldo);
                        dto.Ganacias = Double.Parse(String.Format(dto.Ganacias % 1 == 0 ? "{0:0}" : "{0:0.00}", dto.Ganacias));
                        return dto;
                    }
                ).ToList();
                return ctnAhorros != null ? ctn : null;
            }

            public double GetInteres(double saldo)
            {
                if (saldo>= 0 && saldo<=5000)
                {
                    return 0.25;
                }else if (saldo>=5000.01 && saldo<=60000)
                {
                    return 0.50;
                }else if (saldo>=60000.01 && saldo<=120000)
                {
                    return 0.75;
                } else if (saldo>=120000.01)
                {
                    return 1.50;
                }
                else
                {
                    return 0.0;
                }
            }
            
            public double CalcularGanancias(int numeroMeses,double TasaInteresMensual, double Saldo)
            {
                TasaInteresMensual = TasaInteresMensual * 100;
                        decimal meses = (decimal)numeroMeses;
                        decimal n = (1m / 12m) * (meses);
                        decimal i = (decimal)Convert.ToDecimal(TasaInteresMensual) / 100m;
                        decimal resultado = (decimal)(Convert.ToDecimal(Saldo) * n * i);
            
                        return (double) resultado;
                    }
            
        }
    }
}
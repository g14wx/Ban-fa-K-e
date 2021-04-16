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

namespace lab2.Application.Queries.TimeDeposit
{
    public class GetAllTimeDepositFromIdBankAccount
    {
        public record Query(int IdCuentaBancaria) : IRequest<List<DepositoAPlazoDTO>>;
        
        public class Handler : IRequestHandler<Query,List<DepositoAPlazoDTO>>
        {
            private ITimeDepositRepository _repository;
            private IMapper _mapper;
            public Handler(ITimeDepositRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }
            public async Task<List<DepositoAPlazoDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                List<DepositoAPlazo> dps =await _repository.getAllTimeDepositByIdBankAccount(request.IdCuentaBancaria);
                List<DepositoAPlazoDTO> dapdto = _mapper.Map<List<DepositoAPlazo>, List<DepositoAPlazoDTO>>(dps);
                dapdto = dapdto.Select(dto =>
                {
                     dto.Ganacias = CalcularGanancias(dto.CantidadDias, dto.TasaInteres, dto.Cantidad);
                     dto.Ganacias = Double.Parse(String.Format(dto.Ganacias % 1 == 0 ? "{0:0}" : "{0:0.00}", dto.Ganacias));
                     return dto;
                }).ToList();
                return dapdto;
            }
            
        private double CalcularGanancias(int dias, double TasaInteres, double Cantidad)
        {
            TasaInteres = TasaInteres * 100;
            decimal n = (1m / 365m) * (decimal) dias; 
            decimal i = (decimal)Convert.ToDecimal(TasaInteres) / 100m;
            decimal resultado = (decimal)(Convert.ToDecimal(Cantidad) * n * i);

            // CALCULAR meses cuenta de ahorro
            
            return (double) resultado; // ganancias
        }
        }
    }
}
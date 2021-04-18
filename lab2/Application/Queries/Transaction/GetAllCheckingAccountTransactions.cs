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

namespace lab2.Application.Queries.Transaction
{
    public class GetAllCheckingAccountTransactions
    {
        public record Query(int IdCuentaCorriente) : IRequest<List<TransactionsDTO>>;

        public class Handler : IRequestHandler<Query, List<TransactionsDTO>>
        {
            private ITransactionRepository _repository;
            private IMapper _mapper;
            public Handler(ITransactionRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }
            public async Task<List<TransactionsDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                List<Transaccion> transactions = await _repository.GetAllTransactionByCheckingAccount(request.IdCuentaCorriente);
                List<TransactionsDTO> transactionsDTOs = _mapper.Map<List<Transaccion>, List<TransactionsDTO>>(transactions);
                transactionsDTOs = transactionsDTOs.Select(dto =>
                {
                    dto.Fecha = dto.Fecha;
                    dto.Cantidad = dto.Cantidad;
                    dto.Saldo = Double.Parse(String.Format(dto.Saldo % 1 == 0 ? "{0:0}" : "{0:0.00}", dto.Saldo));
                    dto.Tipo = dto.Tipo;
                    return dto;
                }
                ).ToList();
                return transactions != null ? transactionsDTOs : null;
            }


        }
    }
}

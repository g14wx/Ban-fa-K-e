using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EfModels;
using lab2.Domain.Contracts;
using lab2.Domain.Models;

namespace lab2.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private MyDbContext _repository;
        private IMapper _mapper;

        public TransactionRepository(MyDbContext repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<Transaccion>> GetAllTransactionByCheckingAccount(int IdCheckingAccount)
        {
            List<EfModels.Models.Transaccion> transactions =
               _repository.Transacciones.Where(t => t.IdCuentaCorriente == IdCheckingAccount).ToList();
            List<Transaccion> list = _mapper.Map<List<EfModels.Models.Transaccion>, List<Transaccion>>(transactions);
            return list;
        }

        public async Task<List<Transaccion>> GetAllTransactionBySavingAccount(int IdSavingAccount)
        {
            List<EfModels.Models.Transaccion> transactions =
               _repository.Transacciones.Where(t => t.IdCuentaAhorro == IdSavingAccount).ToList();
            List<Transaccion> list = _mapper.Map<List<EfModels.Models.Transaccion>, List<Transaccion>>(transactions);
            return list;
        }

        public async Task<bool> RegisterTransaction(Transaccion transaccion)
        {
            EfModels.Models.Transaccion trsn = _mapper.Map<EfModels.Models.Transaccion>(transaccion);
            await _repository.Transacciones.AddAsync(trsn);
            await _repository.SaveChangesAsync();
            return true;
        }

    }
}

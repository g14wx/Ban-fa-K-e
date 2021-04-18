using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using lab2.Domain.Models;

namespace lab2.Domain.Contracts
{
    public interface ITransactionRepository
    {
        public Task<List<Transaccion>> GetAllTransactionBySavingAccount(int IdSavingAccount);
        public Task<List<Transaccion>> GetAllTransactionByCheckingAccount(int IdCheckingAccount);

        public Task<bool> RegisterTransaction(Transaccion transaccion);
    }
}

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

        public Task<Dictionary<string,string>> RegisterTransaction(Transaccion transaccion);
        
         public Task<Double> SumCheckingAccountTodayTransactions(int IdCheckingAccount);

         public Task<Double> SumSavingAccountTodayTransactions(int IdSavingAccount);
    }
}

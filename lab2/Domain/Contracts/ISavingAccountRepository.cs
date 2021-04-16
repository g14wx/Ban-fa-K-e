using System.Collections.Generic;
using System.Threading.Tasks;
using lab2.Domain.Models;

namespace lab2.Domain.Contracts
{
    public interface ISavingAccountRepository
    {
        
        public Task<List<CuentaAhorro>> GetAllSavingAccountFromABankAccount(int IdBankAccount);
        public Task<CuentaAhorro> FindSavingAccount(int Id);

        public Task<CuentaAhorro> CreateSavingAccount(CuentaAhorro user);
    }
}
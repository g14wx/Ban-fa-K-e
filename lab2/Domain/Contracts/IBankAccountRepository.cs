using System.Collections.Generic;
using System.Threading.Tasks;
using lab2.Domain.Models;

namespace lab2.Domain.Contracts
{
    public interface IBankAccountRepository
    {
        public Task<List<CuentaBancaria>> GetAllBankAccountFromAUser(int IdUser);
        public Task<CuentaBancaria> CreateAccountAsync(CuentaBancaria cuentaBancaria);
        public Task<CuentaBancaria> FindBankAccount(int IdBankAccount);
    }
}
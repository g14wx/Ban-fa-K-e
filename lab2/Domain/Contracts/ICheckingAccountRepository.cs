using System.Collections.Generic;
using System.Threading.Tasks;
using lab2.Domain.Models;

namespace lab2.Domain.Contracts
{
    public interface ICheckingAccountRepository
    {
        

        public Task<CuentaCorriente> GetCheckingAccountById(int IdCheckingAccount);
        public Task<List<CuentaCorriente>> GetAllCheckingAccountByBankAccountId(int IdBankAccount);
        public Task<CuentaCorriente> CreateNewCheckingAccount(CuentaCorriente cuentaCorriente);
        public Task<bool> DeleteCheckingAccount(int IdCheckingAccount);

        public Task<bool> WithDrawMoney(int IdCheckingAccount, double Amount);
        public Task<bool> CreditMoney(int IdCheckingAccount, double Amount);    }
}
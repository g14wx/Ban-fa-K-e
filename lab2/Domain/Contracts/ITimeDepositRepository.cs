using System.Collections.Generic;
using System.Threading.Tasks;
using lab2.Domain.Models;

namespace lab2.Domain.Contracts
{
    public interface ITimeDepositRepository
    {
        public Task<DepositoAPlazo> getTimeDepositById(int IdTimeDeposit);
        public Task<List<DepositoAPlazo>> getAllTimeDepositByIdBankAccount(int IdBankAccount);

        public Task<DepositoAPlazo> CreateTimeDeposit(DepositoAPlazo depositoAPlazo);
    }
}
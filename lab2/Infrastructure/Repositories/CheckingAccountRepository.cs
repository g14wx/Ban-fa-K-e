using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EfModels;
using lab2.Domain.Contracts;
using lab2.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace lab2.Infrastructure.Repositories
{
    public class CheckingAccountRepository : ICheckingAccountRepository
    {
        
private IMapper _mapper;
        private MyDbContext _db;
        public CheckingAccountRepository(MyDbContext context, IMapper mapper)
        {
            _db = context;
            _mapper = mapper;
        }
        
        public async Task<CuentaCorriente> GetCheckingAccountById(int IdCheckingAccount)
        {
            EfModels.Models.ProductosFinancieros.CuentaCorriente checkingAccountFound =
                _db.CuentaCorrientes.FirstOrDefault( ch => ch.Id==IdCheckingAccount);
            return _mapper.Map<CuentaCorriente>(checkingAccountFound);
        }

        public async Task<List<CuentaCorriente>> GetAllCheckingAccountByBankAccountId(int IdBankAccount)
        {
            List<EfModels.Models.ProductosFinancieros.CuentaCorriente> checkingAccounts = _db.CuentaCorrientes
                .Where(ch => ch.IdCuentaBancaria == IdBankAccount).ToList();
            return _mapper.Map<List<EfModels.Models.ProductosFinancieros.CuentaCorriente>, List<CuentaCorriente>>(
                checkingAccounts);
        }

        public async Task<CuentaCorriente> CreateNewCheckingAccount(CuentaCorriente cuentaCorriente)
        {
            EfModels.Models.ProductosFinancieros.CuentaCorriente newCheckingAccount =
                _mapper.Map<EfModels.Models.ProductosFinancieros.CuentaCorriente>(cuentaCorriente);
            await _db.CuentaCorrientes.AddAsync(newCheckingAccount);
            await _db.SaveChangesAsync();
            return _mapper.Map<CuentaCorriente>(newCheckingAccount);
        }

        public async Task<bool> DeleteCheckingAccount(int IdCheckingAccount)
        {
            EfModels.Models.ProductosFinancieros.CuentaCorriente ch =
                _db.CuentaCorrientes.FirstOrDefault(ch => ch.Id == IdCheckingAccount);
            _db.CuentaCorrientes.Attach(ch);
            _db.Entry(ch).State = EntityState.Deleted;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> WithDrawMoney(int IdCheckingAccount, double Amount)
        {
            EfModels.Models.ProductosFinancieros.CuentaCorriente checkingAccount =
                _db.CuentaCorrientes.FirstOrDefault(ch => ch.Id == IdCheckingAccount);
            checkingAccount.Saldo -= Amount;
            _db.Update(checkingAccount);
            return true;
        }
        public async Task<bool> CreditMoney(int IdCheckingAccount, double Amount)
        {
            EfModels.Models.ProductosFinancieros.CuentaCorriente checkingAccount =
                _db.CuentaCorrientes.FirstOrDefault(ch => ch.Id == IdCheckingAccount);
            checkingAccount.Saldo += Amount;
            _db.Update(checkingAccount);
            return true;
        }
    }    
}

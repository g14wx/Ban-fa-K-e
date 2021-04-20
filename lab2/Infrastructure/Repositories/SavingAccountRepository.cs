using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EfModels;
using lab2.Domain.Contracts;
using lab2.Domain.Models;
using CuentaBancaria = EfModels.Models.CuentaBancaria;

namespace lab2.Infrastructure.Repositories
{
    public class SavingAccountRepository: ISavingAccountRepository
    {
        private MyDbContext _db;
        private IMapper _mapper;
        public SavingAccountRepository(MyDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _db = context;
        }
        public async Task<List<CuentaAhorro>> GetAllSavingAccountFromABankAccount(int IdBankAccount)
        {

            List<EfModels.Models.ProductosFinancieros.CuentaAhorro> cuentasAhorros =
             _db.CuentaAhorros.Where(c => c.IdCuentaBancaria == IdBankAccount).ToList();
            List<CuentaAhorro> accounts= _mapper.Map<List<EfModels.Models.ProductosFinancieros.CuentaAhorro>, List<CuentaAhorro>>(cuentasAhorros);
            return accounts;
        }

        public async Task<CuentaAhorro> FindSavingAccount(int IdSavingAccount)
        {

            EfModels.Models.ProductosFinancieros.CuentaAhorro cuentaAhorro =
                _db.CuentaAhorros.FirstOrDefault(c => c.Id == IdSavingAccount);
            return _mapper.Map<CuentaAhorro>(cuentaAhorro);
        }

        public async Task<CuentaAhorro> CreateSavingAccount(CuentaAhorro cuentaAhorro)
        {
            cuentaAhorro.IsActive = true;
            EfModels.Models.ProductosFinancieros.CuentaAhorro savingAccount = _mapper
                .Map<EfModels.Models.ProductosFinancieros.CuentaAhorro>(cuentaAhorro);
            await _db.CuentaAhorros.AddAsync(savingAccount);
            await _db.SaveChangesAsync();
            return _mapper.Map<CuentaAhorro>(savingAccount);
        }

        public async Task<bool> DesactivarCuenta(int IdCheckingAccount)
        {
            EfModels.Models.ProductosFinancieros.CuentaAhorro checkingAccount =
                        _db.CuentaAhorros.FirstOrDefault(ch => ch.Id == IdCheckingAccount);
            checkingAccount.IsActive = false;
            _db.CuentaAhorros.Attach(checkingAccount);
            _db.Entry(checkingAccount).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> WithDrawMoney(int IdCheckingAccount, double Amount)
                {
                    EfModels.Models.ProductosFinancieros.CuentaCorriente checkingAccount =
                        _db.CuentaCorrientes.FirstOrDefault(ch => ch.Id == IdCheckingAccount);
                    if (Amount <= 400.00 && checkingAccount.Dialy <= 1000.00)
                    {
                        checkingAccount.Saldo -= Amount;
                        checkingAccount.Dialy -= Amount;
                        if (checkingAccount.Saldo <= 1.0)
                        {
                            checkingAccount.IsActive = false;
                        }
                        _db.Update(checkingAccount);
                        await _db.SaveChangesAsync();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
        
                }
                public async Task<bool> CreditMoney(int IdCheckingAccount, double Amount)
                {
                    EfModels.Models.ProductosFinancieros.CuentaCorriente checkingAccount =
                        _db.CuentaCorrientes.FirstOrDefault(ch => ch.Id == IdCheckingAccount);
        
                        checkingAccount.Saldo += Amount;
                        _db.Update(checkingAccount);
                        await _db.SaveChangesAsync();
                        return true;
               }
    }
}
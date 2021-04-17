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
    public class BankAccountRepository: IBankAccountRepository
    {
        private MyDbContext _repository;
        private IMapper _mapper;
        public BankAccountRepository(IMapper mapper,MyDbContext repository)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<List<CuentaBancaria>> GetAllBankAccountFromAUser(int IdUser)
        {
            List<EfModels.Models.CuentaBancaria> cuentaBancarias =
                _repository.CuentaBancarias.Where(c => c.IdUser == IdUser).ToList();
                List<CuentaBancaria> cntBancaria= _mapper.Map<List<EfModels.Models.CuentaBancaria>, List<CuentaBancaria>>(cuentaBancarias);
                return cntBancaria;
        }

        public async Task<CuentaBancaria> CreateAccountAsync(CuentaBancaria cuentaBancaria)
        {
            EfModels.Models.CuentaBancaria ctn = _mapper.Map<EfModels.Models.CuentaBancaria>(cuentaBancaria);
            await _repository.CuentaBancarias.AddAsync(ctn);
            await _repository.SaveChangesAsync();
            return _mapper.Map<CuentaBancaria>(ctn);
        }

        public async Task<CuentaBancaria> FindBankAccount(int IdBankAccount)
        {
            EfModels.Models.CuentaBancaria cntBancaria =
                _repository.CuentaBancarias.FirstOrDefault(c => c.Id == IdBankAccount);
            return _mapper.Map<CuentaBancaria>(cntBancaria);
        }

        public async Task<CuentaBancaria> FindBankAccountByNAccount(string NAccount,String Pin)
        {
            EfModels.Models.CuentaBancaria account =
                _repository.CuentaBancarias.FirstOrDefault(x => x.NCuenta == NAccount && x.Pin == Pin );
            if (account != null)
            {
                return _mapper.Map<CuentaBancaria>(account);
            }
            else
            {
                return null;
            }
        }
    }
}
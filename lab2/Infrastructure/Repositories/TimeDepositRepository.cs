using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EfModels;
using lab2.Domain.Contracts;
using lab2.Domain.Models;

namespace lab2.Infrastructure.Repositories
{
    public class TimeDepositRepository : ITimeDepositRepository
    {
        private MyDbContext _db;
        private IMapper _mapper;
        public TimeDepositRepository(MyDbContext context, IMapper mapper)
        {
            _db = context;
            _mapper = mapper;
        }
        public async Task<DepositoAPlazo> getTimeDepositById(int IdTimeDeposit)
        {
            EfModels.Models.ProductosFinancieros.DepositoAPlazo timeDeposit =
                _db.DepositoAPlazos.FirstOrDefault(dp => dp.Id == IdTimeDeposit);
            return _mapper.Map<DepositoAPlazo>(timeDeposit);
        }

        public async Task<List<DepositoAPlazo>> getAllTimeDepositByIdBankAccount(int IdBankAccount)
        {
            List<EfModels.Models.ProductosFinancieros.DepositoAPlazo> TimeDepositsAccounts =
                _db.DepositoAPlazos.Where(dp => dp.IdCuentaBancaria == IdBankAccount).ToList();
            return _mapper.Map< List<EfModels.Models.ProductosFinancieros.DepositoAPlazo>,List<DepositoAPlazo>>(
                TimeDepositsAccounts);
        }

        public async Task<DepositoAPlazo> CreateTimeDeposit(DepositoAPlazo depositoAPlazo)
        {
            EfModels.Models.ProductosFinancieros.DepositoAPlazo dp =
                _mapper.Map<EfModels.Models.ProductosFinancieros.DepositoAPlazo>(depositoAPlazo);
            await _db.DepositoAPlazos.AddAsync(dp);
            await _db.SaveChangesAsync();
            return _mapper.Map<DepositoAPlazo>(dp);
        }
    }
}
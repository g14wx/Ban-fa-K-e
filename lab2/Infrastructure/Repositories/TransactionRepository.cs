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
    public class TransactionRepository : ITransactionRepository
    {
        private MyDbContext _repository;
        private IMapper _mapper;

        public TransactionRepository(MyDbContext repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<Transaccion>> GetAllTransactionByCheckingAccount(int IdCheckingAccount)
        {
            List<EfModels.Models.Transaccion> transactions =
               _repository.Transacciones.Where(t => t.IdCuentaCorriente == IdCheckingAccount).ToList();
            List<Transaccion> list = _mapper.Map<List<EfModels.Models.Transaccion>, List<Transaccion>>(transactions);
            return list;
        }

        public async Task<Double> SumCheckingAccountTodayTransactions(int IdCheckingAccount)
        {
            List<EfModels.Models.Transaccion> transactions =
               _repository.Transacciones.Where(t => t.IdCuentaCorriente == IdCheckingAccount && t.Fecha.Date == DateTime.Now.Date && t.Tipo == 2).ToList();
            List<Transaccion> list = _mapper.Map<List<EfModels.Models.Transaccion>, List<Transaccion>>(transactions);
            
            return list.Sum(item => item.Cantidad); ;
        }

        

        public async Task<List<Transaccion>> GetAllTransactionBySavingAccount(int IdSavingAccount)
        {
            List<EfModels.Models.Transaccion> transactions =
               _repository.Transacciones.Where(t => t.IdCuentaAhorro == IdSavingAccount).ToList();
            List<Transaccion> list = _mapper.Map<List<EfModels.Models.Transaccion>, List<Transaccion>>(transactions);
            return list;
        }

        public async Task<Double> SumSavingAccountTodayTransactions(int IdSavingAccount)
        {
            List<EfModels.Models.Transaccion> transactions =
               _repository.Transacciones.Where(t => t.IdCuentaAhorro == IdSavingAccount && t.Fecha.Date == DateTime.Now.Date && t.Tipo == 2).ToList();
            List<Transaccion> list = _mapper.Map<List<EfModels.Models.Transaccion>, List<Transaccion>>(transactions);
            return list.Sum(item => item.Cantidad); ;
        }

        public async Task<Dictionary<string, string>> RegisterTransaction(Transaccion transaccion)
        {
            if (transaccion.Tipo == 1)
            {
                EfModels.Models.Transaccion trsn = _mapper.Map<EfModels.Models.Transaccion>(transaccion);
                await _repository.Transacciones.AddAsync(trsn);
                await _repository.SaveChangesAsync();
                return new Dictionary<string, string>()
                {
                    { "res", "success" },
                    { "msg", ""}
                };
            }
            else
            {
                Dictionary<string, string> response = await EvaluarCantidadRetiro(transaccion);
                if (response["res"] == "success")
                {
                    EfModels.Models.Transaccion trsn = _mapper.Map<EfModels.Models.Transaccion>(transaccion);
                    await _repository.Transacciones.AddAsync(trsn);
                    await _repository.SaveChangesAsync();
                }

                return response;
            }
            

        }

        private async Task<Dictionary<string, string>> EvaluarCantidadRetiro(Transaccion t)
        {
            if (t.Cantidad > 400)
            {
                return new Dictionary<string, string>()
                {
                    { "res", "error" },
                    { "msg", "La cantidad es mayor a $400"}
                };
            }

            Double sumaDiaria = 0;

            if (t.IdCuentaAhorro > 0)
            {
                sumaDiaria = await SumSavingAccountTodayTransactions(t.IdCuentaAhorro);
            }
            else
            {
                sumaDiaria = await SumCheckingAccountTodayTransactions(t.IdCuentaCorriente);
            }

            if (sumaDiaria >= 1000)
            {
                return new Dictionary<string, string>()
                {
                    { "res", "error" },
                    { "msg", "La cantidad supera al maximo de retiro por dia ($1000)"}
                };
            }

            if (t.Saldo<0)
            {
                return new Dictionary<string, string>()
                {
                    { "res", "error" },
                    { "msg", "La cantidad es mayor al saldo actual"}
                };
            }

            if (t.Saldo >= 0 && t.Saldo < 1)
            {
                return new Dictionary<string, string>()
                {
                    { "res", "error" },
                    { "msg", "Debe indicar si desea proceder"}
                };
            }

            return new Dictionary<string, string>()
            {
                { "res", "success" },
                { "msg", "La cantidad es correcta"}
            };
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using lab2.Domain.Contracts;
using lab2.Domain.DTOs;
using lab2.Domain.Models;
using lab2.Presentation.Account;
using MediatR;

namespace lab2.Application.Queries.BankAccount
{
    public class GetAcccountInformation
    {
        public record Query(int IdAccount, int IdUser) : IRequest<AccountProductsViewModel>;

        public class Handler : IRequestHandler<Query, AccountProductsViewModel>
        {
            private IBankAccountRepository _bankAccountRepository;
            private IUserRepository _userRepository;
            private ISavingAccountRepository _savingAccountRepository;
            private ICheckingAccountRepository _checkingAccount;
            private ITimeDepositRepository _timeDepositRepository;
            private ITransactionRepository _transactionRepository;
            private IMapper _mapper;

            public Handler(IMapper mapper, IUserRepository userRepository, IBankAccountRepository bankAccountRepository,
                ISavingAccountRepository savingAccountRepository, ICheckingAccountRepository checkingAccount,
                ITimeDepositRepository timeDepositRepository, ITransactionRepository transactionRepository)
            {
                _userRepository = userRepository;
                _bankAccountRepository = bankAccountRepository;
                _savingAccountRepository = savingAccountRepository;
                _checkingAccount = checkingAccount;
                _timeDepositRepository = timeDepositRepository;
                _transactionRepository = transactionRepository;
                _mapper = mapper;
            }

            public async Task<AccountProductsViewModel> Handle(Query request, CancellationToken cancellationToken)
            {
                Domain.Models.User user = await _userRepository.FindUserByIdAsync(request.IdUser);
                CuentaBancaria cntBancaria = await _bankAccountRepository.FindBankAccount(request.IdAccount);
                user.CntBancaria = cntBancaria;
                List<CuentaAhorro> SavingAccounts =
                    await _savingAccountRepository.GetAllSavingAccountFromABankAccount(request.IdAccount);
                List<CuentaCorriente> chekingAccounts =
                    await _checkingAccount.GetAllCheckingAccountByBankAccountId(request.IdAccount);
                List<DepositoAPlazo> TimeDepositAccounts =
                    await _timeDepositRepository.getAllTimeDepositByIdBankAccount(request.IdAccount);

                List<DepositoAPlazoDTO> dpDTO =
                    _mapper.Map<List<DepositoAPlazo>, List<DepositoAPlazoDTO>>(TimeDepositAccounts);

                List<CuentaAhorroDTO> cadto = _mapper.Map<List<CuentaAhorro>, List<CuentaAhorroDTO>>(SavingAccounts);

                chekingAccounts = chekingAccounts.Select(checkingAccount =>
                {
                    checkingAccount.Saldo = getRealBalanceFromCheckingAccountById(checkingAccount.Id);
                    return checkingAccount;
                }).ToList();
                
                cadto = cadto.Select((dto) =>
                {
                    dto.TasaInteresMensual = GetInteres(dto.Saldo);
                    dto.Ganacias = CalcularGananciasCuentaAhorro(1, dto.TasaInteresMensual, dto.Saldo);
                    dto.Ganacias =
                        Double.Parse(String.Format(dto.Ganacias % 1 == 0 ? "{0:0}" : "{0:0.00}", dto.Ganacias));
                    // getting real balance from every account
                    dto.Saldo = getRealBalanceFromSavingAccountById(dto.Id);
                    return dto;
                }).ToList();

                dpDTO = dpDTO.Select(dto =>
                {
                    dto.Ganacias = CalcularGanancias(dto.CantidadDias, dto.TasaInteres, dto.Cantidad);
                    dto.Ganacias =
                        Double.Parse(String.Format(dto.Ganacias % 1 == 0 ? "{0:0}" : "{0:0.00}", dto.Ganacias));
                    return dto;
                }).ToList();

                double total = SavingAccounts.Aggregate(0.0, (accum, ahorro) => accum + ahorro.Saldo);
                total += chekingAccounts.Aggregate(0.0, (accum, corriente) => accum + corriente.Saldo);
                total += TimeDepositAccounts.Aggregate(0.0, (accum, plazo) => plazo.Cantidad);

                return new AccountProductsViewModel()
                {
                    user = user,
                    cuentaBancaria = cntBancaria,
                    CuentasAhorro = cadto,
                    CuentaCorrientes = chekingAccounts,
                    totalAccounts = SavingAccounts.Count + chekingAccounts.Count + TimeDepositAccounts.Count,
                    DepositoAPlazos = dpDTO,
                    total = total
                };
            }

            private double CalcularGanancias(int dias, double TasaInteres, double Cantidad)
            {

                TasaInteres = TasaInteres * 100;
                decimal n = (1m / 365m) * (decimal) dias;
                decimal i = (decimal) Convert.ToDecimal(TasaInteres) / 100m;
                decimal resultado = (decimal) (Convert.ToDecimal(Cantidad) * n * i);

                // CALCULAR meses cuenta de ahorro

                return (double) resultado; // ganancias
            }

            public double CalcularGananciasCuentaAhorro(int numeroMeses, double TasaInteresMensual, double Saldo)
            {
                TasaInteresMensual = TasaInteresMensual * 100;
                decimal meses = (decimal) numeroMeses;
                decimal n = (1m / 12m) * (meses);
                decimal i = (decimal) Convert.ToDecimal(TasaInteresMensual) / 100m;
                decimal resultado = (decimal) (Convert.ToDecimal(Saldo) * n * i);

                return (double) resultado;
            }

            public double GetInteres(double saldo)
            {
                if (saldo >= 0 && saldo <= 5000)
                {
                    return 0.25;
                }
                else if (saldo >= 5000.01 && saldo <= 60000)
                {
                    return 0.50;
                }
                else if (saldo >= 60000.01 && saldo <= 120000)
                {
                    return 0.75;
                }
                else if (saldo >= 120000.01)
                {
                    return 1.50;
                }
                else
                {
                    return 0.0;
                }
            }

            public double getRealBalanceFromSavingAccountById(int IdSavingAccount)
            {
                List<Transaccion> transaccions= _transactionRepository.GetAllTransactionBySavingAccount(IdSavingAccount).Result;
                return transaccions[transaccions.Count-1].Saldo;
            }
            public double getRealBalanceFromCheckingAccountById(int IdCheckingAccount)
            {
                List<Transaccion> transaccions= _transactionRepository.GetAllTransactionByCheckingAccount(IdCheckingAccount).Result;
                return transaccions[transaccions.Count-1].Saldo;
            }
        }
    }
}
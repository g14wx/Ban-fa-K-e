using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using lab2.Domain.Contracts;
using lab2.Domain.DTOs;
using lab2.Domain.Models;
using lab2.Presentation.Account;
using MediatR;

namespace lab2.Application.Queries.BankAccount
{
    public class GetAcccountInformation
    {
        public record Query(int IdAccount, int IdUser): IRequest<AccountProductsViewModel>;
        
        public class Handler : IRequestHandler<Query,AccountProductsViewModel>
        {
            private IBankAccountRepository _bankAccountRepository;
            private IUserRepository _userRepository;
            private ISavingAccountRepository _savingAccountRepository;
            private ICheckingAccountRepository _checkingAccount;
            private ITimeDepositRepository _timeDepositRepository;
            
            public Handler(IUserRepository userRepository, IBankAccountRepository bankAccountRepository, ISavingAccountRepository savingAccountRepository, ICheckingAccountRepository checkingAccount, ITimeDepositRepository timeDepositRepository)
            {
                _userRepository = userRepository;
                _bankAccountRepository = bankAccountRepository;
                _savingAccountRepository = savingAccountRepository;
                _checkingAccount = checkingAccount;
                _timeDepositRepository = timeDepositRepository;
            }
            public async Task<AccountProductsViewModel> Handle(Query request, CancellationToken cancellationToken)
            {
                Domain.Models.User user = await _userRepository.FindUserByIdAsync(request.IdUser);
                CuentaBancaria cntBancaria = await _bankAccountRepository.FindBankAccount(request.IdAccount);
                List<CuentaAhorro> SavingAccounts =
                    await _savingAccountRepository.GetAllSavingAccountFromABankAccount(request.IdAccount);
                List<CuentaCorriente> chekingAccounts =
                    await _checkingAccount.GetAllCheckingAccountByBankAccountId(request.IdAccount);
                List<DepositoAPlazo> TimeDepositAccounts =
                    await _timeDepositRepository.getAllTimeDepositByIdBankAccount(request.IdAccount);
                
                double total = SavingAccounts.Aggregate(0.0, (accum, ahorro) => accum + ahorro.Saldo);
                total += chekingAccounts.Aggregate(0.0, (accum, corriente) => accum + corriente.Saldo);
                total += TimeDepositAccounts.Aggregate(0.0, (accum, plazo) => plazo.Cantidad);
                
                return new AccountProductsViewModel()
                {
                    user = user,
                    cuentaBancaria = cntBancaria,
                    CuentasAhorro = SavingAccounts,
                    CuentaCorrientes = chekingAccounts,
                    totalAccounts = SavingAccounts.Count + chekingAccounts.Count + TimeDepositAccounts.Count,
                    DepositoAPlazos = TimeDepositAccounts,
                    total = total
                };
            }
        }
    }
}
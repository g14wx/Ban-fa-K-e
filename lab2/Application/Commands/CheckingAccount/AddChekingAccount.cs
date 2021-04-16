using System.Threading;
using System.Threading.Tasks;
using lab2.Domain.Contracts;
using lab2.Domain.DTOs;
using lab2.Domain.Models;
using MediatR;

namespace lab2.Application.Commands.CheckingAccount
{
    public class AddChekingAccount
    {
        public record Command(CuentaCorrienteDTO CuentaCorrienteDto) : IRequest<CuentaCorriente>;
        
        public class Handler : IRequestHandler<Command,CuentaCorriente>
        {
            private ICheckingAccountRepository _checkingAccountRepository;
            public Handler(ICheckingAccountRepository repository)
            {
                _checkingAccountRepository = repository;
            }
           
            public async Task<CuentaCorriente> Handle(Command request, CancellationToken cancellationToken)
            {
                CuentaCorriente cc = await _checkingAccountRepository.CreateNewCheckingAccount(new CuentaCorriente
                {
                    Saldo = request.CuentaCorrienteDto.Saldo,
                    IdCuentaBancaria = request.CuentaCorrienteDto.IdCuentaBancaria
                });
                return cc;
            }
        }
    }
}
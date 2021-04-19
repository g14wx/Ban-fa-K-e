using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using lab2.Application.Commands.CheckingAccount;
using lab2.Application.Commands.Transaction;
using lab2.Application.Queries.CheckingAccount;
using lab2.Domain.DTOs;
using lab2.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace lab2.Controllers
{
    [Route("checkingaccount")]
    public class CheckingAccountController : Controller
    {
        private IMediator _mediator;
        public CheckingAccountController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET
        public async Task<ViewResult> Index(int IdBankAccount)
        {
            ViewBag.IdBankAccount = Int16.Parse(Request.Cookies["IdBankAccount"]);
            ViewData["Title"] = "Checking Accounts";
            List<CuentaCorriente> ccs = await _mediator.Send(new GetAllChekingAccountsByIdBankAccount.Query(IdBankAccount)); 
            return View(ccs);
        }

        [HttpPost("")]
        public async Task<RedirectResult> CreateCheckingAccount(CuentaCorrienteDTO cuentaCorrienteDto)
        {
            CuentaCorriente cc = await _mediator.Send(new AddChekingAccount.Command(cuentaCorrienteDto));
            await _mediator.Send(new AddCheckingAccountTransactionCommand.Command(cc.Id,cc.Saldo,1,cc.Saldo, new DateTime()));
            return new RedirectResult($"/bankaccount?IdBankAccount={cuentaCorrienteDto.IdCuentaBancaria}");
        }
        
        
    }
}
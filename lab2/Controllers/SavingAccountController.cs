using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using lab2.Application.Commands.SavingAccount;
using lab2.Application.Queries.SavingAccount;
using lab2.Domain.DTOs;
using lab2.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace lab2.Controllers
{
    [Route("savingaccount")]
    public class SavingAccountController : Controller
    {
        private IMediator _mediator;
        public SavingAccountController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET
        public async Task<ViewResult> Index(int IdBankAccount)
        {
            ViewBag.IdCuentaBancaria = Int16.Parse(Request.Cookies["IdBankAccount"]);
            ViewData["Title"] = "Saving Accounts";
            List<CuentaAhorro> cntAhrAhorros = await _mediator.Send(new GetAllSavingAccounts.Query(IdBankAccount));
            return View(cntAhrAhorros);
        }

        [HttpGet("{idSavingAccount}")]
        public async Task<ViewResult> GetSavingAcccount(int idSavingAccount)
        {
            CuentaAhorro savingAccount = await _mediator.Send(new GetSavingAccount.Query(idSavingAccount));
            return View("SavingAccount",savingAccount);
        }

        [HttpPost("")]
        public async Task<RedirectResult> CreateSavingAccount(CuentaAhorroDTO cadto)
        {
            int id = await _mediator.Send(new AddSavingAccountCommand.Command(cadto.Saldo,cadto.TasaInteresMensual,cadto.IdCuentaBancaria));
            return new RedirectResult($"/bankaccount?IdBankAccount={cadto.IdCuentaBancaria}");
        }
    }
}
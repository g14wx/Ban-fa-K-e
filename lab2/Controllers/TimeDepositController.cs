using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using lab2.Application.Commands.TimeDeposit;
using lab2.Application.Queries.TimeDeposit;
using lab2.Domain.DTOs;
using lab2.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace lab2.Controllers
{
    public class TimeDepositController : Controller
    {
        private IMediator _mediator;

        public TimeDepositController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET
        public async Task<ViewResult> Index(int IdBankAccount)
        {
            ViewBag.IdBankAccount = Int16.Parse(Request.Cookies["IdBankAccount"]);
            ViewData["Title"] = "Time Deposit Accounts";
            List<DepositoAPlazoDTO> ccs = await _mediator.Send(new GetAllTimeDepositFromIdBankAccount.Query(IdBankAccount)); 
            return View(ccs);
        }
        
        [HttpPost("")]
        public async Task<RedirectResult> CreateTimeDepositAccount(DepositoAPlazoDTO depositoAPlazoDto)
        {
            DepositoAPlazo dp = await _mediator.Send(new AddTimeDeposit.Command(depositoAPlazoDto.Cantidad,depositoAPlazoDto.CantidadDias,depositoAPlazoDto.IdCuentaBancaria));
            return new RedirectResult($"/bankaccount/GetAccountProducts/{dp.IdCuentaBancaria}");
        }
    }
}
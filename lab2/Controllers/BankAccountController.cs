using System;
using System.Threading.Tasks;
using lab2.Application;
using lab2.Application.Commands.BankAccount;
using lab2.Application.Queries.BankAccount;
using lab2.Domain.DTOs;
using lab2.Presentation.Account;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace lab2.Controllers
{
    [Route("bankaccount")]
    public class BankAccountController : Controller
    {
        private readonly IMediator _mediator;
        public BankAccountController(IMediator mediator)
        {
            _mediator = mediator;
        }
    [HttpGet("{idUser}")]
        public async Task<ViewResult> Index(int idUser)
        {
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Append("IdUser",idUser.ToString(),option);
            
            ViewData["Title"] = $"Accounts of User with Id {idUser}";
            ViewBag.IdUser = idUser;
            var cuentaBancarias = await _mediator.Send(new GetAllBankAccounts.Query(idUser));
            return View(cuentaBancarias);
        }

        [HttpGet("{idUser}/{status}")]
                public async Task<ViewResult> Index(int idUser,Status status)
                {
                    ViewBag.STATUSUSERS = status;
                    ViewData["Title"] = $"Accounts of User with Id {idUser}";
                    ViewBag.IdUser = idUser;
                    var cuentaBancarias = await _mediator.Send(new GetAllBankAccounts.Query(idUser));
                    return View(cuentaBancarias);
                }
        [HttpPost("")]
        public async Task<RedirectResult> CreateAccount( BankAccountDTO Account)
        {
            var response = await _mediator.Send(new AddBankAccountCommand.Command(Account.Pin, Account.IdUser));
            var status = response != null ? Status.CREATED : Status.IDLE;
            return new RedirectResult($"/bankaccount/{Account.IdUser}/{status}");
        }

        public async Task<ViewResult> GetAccountProducts(int IdBankAccount)
        {
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Append("IdBankAccount",IdBankAccount.ToString(),option);
            
            //get iduser from cookie
            String idUserString = Request.Cookies["IdUser"];
            // get products accounts 
            AccountProductsViewModel model = await
                _mediator.Send(new GetAcccountInformation.Query(IdBankAccount, Int32.Parse(idUserString ?? string.Empty)));
            
            return View(model);
        }
    }
}
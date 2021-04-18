using System;
using System.Linq;
using System.Threading.Tasks;
using EfModels;
using lab2.Application.Commands.ATM;
using lab2.Application.Queries.ATM;
using lab2.Application.Queries.BankAccount;
using lab2.Application.Queries.SavingAccount;
using lab2.Domain.DTOs;
using lab2.Domain.Models;
using lab2.Presentation.Account;
using lab2.Presentation.ATM;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CuentaCorriente = EfModels.Models.ProductosFinancieros.CuentaCorriente;

namespace lab2.Controllers
{
    [Route("ATM")]
    public class ATMController : Controller
    {
        private IMediator _mediator;
        private MyDbContext _db;
        public ATMController(IMediator mediator, MyDbContext context)
        {
            _mediator = mediator;
            _db = context;
        }
        // GET
        [HttpGet("")]
        public ViewResult Index()
        {
            return View();
        }

        [HttpGet("{IdBankAccount}")]
        public async Task<IActionResult> ShowATM(int IdBankAccount)
        {
            ViewData["Title"] = "Accounts"; 
            if (Request.Cookies["IdBankAccountATM"] == "" || Request.Cookies["IdBankAccountATM"] == null)
            {
                return new RedirectResult("/ATM/");
            }

            if (IdBankAccount == 0 || IdBankAccount == null)
            {
                IdBankAccount = Int16.Parse(Request.Cookies["IdBankAccountATM"]);
            }
             //get iduser from cookie
            String idUserString = Request.Cookies["IdUserATM"];
            // get products accounts 
            AccountProductsViewModel model = await
                _mediator.Send(new GetAcccountInformation.Query(IdBankAccount, Int32.Parse(idUserString ?? string.Empty)));
            return View(model);
        }

        [HttpGet("account/{idAccount}/{account}")]
        public async Task<IActionResult> ShowAccounts(int IdAccount, String account)
        {
            AccountDTO act = new AccountDTO();
            switch (account)
            {
                case "Saving":
                   CuentaAhorro ct = await _mediator.Send(new GetSavingAccount.Query(IdAccount));
                    act = new AccountDTO()
                    {
                        Amount = ct.Saldo
                    };
                    break;
                case "Checking":
                    CuentaCorriente s = _db.CuentaCorrientes.FirstOrDefault(x => x.Id == IdAccount); 
                act = new AccountDTO()
                {
                    Amount =s.Saldo
                };
                break;
            }
            ViewData["Title"] = "ATM";
            ViewBag.Account = account;
            ViewBag.IdAccount = IdAccount;
            return View(act);
        }

        [HttpPost("")]
        public async Task<IActionResult> loginPost(LoginDTO login)
        {
            CuentaBancaria result = await _mediator.Send(new LoginQuery.Query(login.NCuenta,login.Pin));
            if (result != null)
            {
                Response.Cookies.Append("IdBankAccountATM",result.Id.ToString());
                Response.Cookies.Append("IdUserATM",result.IdUser.ToString());
                return new RedirectResult($"/ATM/{result}");
            }
            else
            {
                @ViewBag.ErrorLogin = true;
                return View("Index");
            }
        }

        [HttpPost("Credit")]
        public async Task<IActionResult> Credit(TransaccionDTO transaccionDto)
        {
            bool response = true;
            switch (transaccionDto.Account)
            {
                case "Saving":
                    response = await _mediator.Send(new CreditSaving.Command(transaccionDto.IdAccount, transaccionDto.Amount));
                    break;
                case "Checking" :
                    response = await _mediator.Send(new Credit.Command(transaccionDto.IdAccount, transaccionDto.Amount));
                    break;
            }

            return new RedirectResult($"account/{transaccionDto.IdAccount}/{transaccionDto.Account}");

        }
        [HttpPost("withdraw")]
        public async Task<IActionResult> WithDraw(TransaccionDTO transaccionDto)
        {
            bool response= true;
                    switch (transaccionDto.Account)
                    {
                        case "Saving":
                            break;
                        case "Checking" :
                            response = await _mediator.Send(new WithDrawChecking.Command(transaccionDto.IdAccount,transaccionDto.Amount));
                            break;
                    }

            return new RedirectResult($"account/{transaccionDto.IdAccount}/{transaccionDto.Account}");
        }

    }
}
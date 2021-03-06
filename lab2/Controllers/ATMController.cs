using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EfModels;
using lab2.Application.Commands.ATM;
using lab2.Application.Commands.Transaction;
using lab2.Application.Queries.ATM;
using lab2.Application.Queries.BankAccount;
using lab2.Application.Queries.SavingAccount;
using lab2.Application.Queries.Transaction;
using lab2.Domain.DTOs;
using lab2.Domain.Models;
using lab2.Presentation.Account;
using lab2.Presentation.ATM;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CuentaAhorro = EfModels.Models.ProductosFinancieros.CuentaAhorro;
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
                    EfModels.Models.ProductosFinancieros.CuentaAhorro ct = _db.CuentaAhorros.FirstOrDefault(x => x.Id == IdAccount);
                    List<lab2.Domain.DTOs.TransactionsDTO> savingList = await _mediator.Send(new GetSavingAccountTransactions.Query(IdAccount));
                    double SaldoSaving = savingList[savingList.Count - 1].Saldo;
                    act = new AccountDTO()
                    {
                        Amount = SaldoSaving
                    };
                    ViewBag.Transactions = savingList;
                    break;
                case "Checking":
                    CuentaCorriente s = _db.CuentaCorrientes.FirstOrDefault(x => x.Id == IdAccount);
                    List<lab2.Domain.DTOs.TransactionsDTO> checkingList = await _mediator.Send(new GetAllCheckingAccountTransactions.Query(IdAccount));
                    double SaldoChecking = checkingList[checkingList.Count - 1].Saldo;
                    act = new AccountDTO()
                    {
                        Amount = SaldoChecking
                    };
                    
                    ViewBag.Transactions = checkingList;
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

            Dictionary<string, string> response = new Dictionary<string, string>();
            switch (transaccionDto.Account)
            {
                case "Saving":
                    response = await _mediator.Send(new AddSavingAccountTransactionCommand.Command(transaccionDto.IdAccount,transaccionDto.Amount,transaccionDto.Tipo,transaccionDto.Saldo + transaccionDto.Amount, DateTime.Now));
                    break;
                case "Checking":
                    response = await _mediator.Send(new AddCheckingAccountTransactionCommand.Command(transaccionDto.IdAccount, transaccionDto.Amount, transaccionDto.Tipo, transaccionDto.Saldo + transaccionDto.Amount, DateTime.Now));
                    break;
            }

            TempData["message"] = response["msg"];
            TempData["status"] = response["res"];
            
            return new RedirectResult($"account/{transaccionDto.IdAccount}/{transaccionDto.Account}"); 
        }
        [HttpPost("withdraw")]
        public async Task<IActionResult> WithDraw(TransaccionDTO transaccionDto)
        {
            Dictionary<string, string> response = new Dictionary<string, string>();
            switch (transaccionDto.Account)
            {
                case "Saving":
                    response = await _mediator.Send(new AddSavingAccountTransactionCommand.Command(transaccionDto.IdAccount, transaccionDto.Amount, transaccionDto.Tipo, transaccionDto.Saldo - transaccionDto.Amount, DateTime.Now));
                    break;
                case "Checking" :
                    response = await _mediator.Send(new AddCheckingAccountTransactionCommand.Command(transaccionDto.IdAccount, transaccionDto.Amount, transaccionDto.Tipo, transaccionDto.Saldo - transaccionDto.Amount, DateTime.Now));
                    break;
            }

            TempData["message"] = response["msg"];
            TempData["status"] = response["res"];
            return new RedirectResult($"account/{transaccionDto.IdAccount}/{transaccionDto.Account}");
        }

        [HttpGet("requestActive/{tipo}/{IdAccount}")]
        public async Task<IActionResult> ActiveSolicitud(String tipo,int IdAccount )
        {
            switch (tipo)
            {
                case "Saving":
                    CuentaAhorro ca= _db.CuentaAhorros.FirstOrDefault(x => x.Id == IdAccount);
                    ca.RequestActive = true;
                    _db.CuentaAhorros.Attach(ca);
                    _db.Entry(ca).State = EntityState.Modified; 
                    await _db.SaveChangesAsync();
                    break;
                
                case "Checking":
                    CuentaCorriente cc= _db.CuentaCorrientes.FirstOrDefault(x => x.Id == IdAccount);
                    cc.RequestActive = true;
                    _db.CuentaCorrientes.Attach(cc);
                    _db.Entry(cc).State = EntityState.Modified; 
                    await _db.SaveChangesAsync();
                    break;
            }

            TempData["mesage"] = "La solitud se ha enviado, para que puedan reactivar su cuenta";
            TempData["status"] = "success";
                    return new RedirectResult("/ATM/0");
        }
        
        [HttpGet("activeAccount/{tipo}/{IdAccount}")]
                public async Task<IActionResult> ActiveAccount(int IdAccount, String tipo)
                {
                    switch (tipo)
                    {
                        case "Saving":
                            CuentaAhorro ca= _db.CuentaAhorros.FirstOrDefault(x => x.Id == IdAccount);
                            ca.RequestActive = false;
                            ca.IsActive = true;
                            _db.CuentaAhorros.Attach(ca);
                            _db.Entry(ca).State = EntityState.Modified; 
                            await _db.SaveChangesAsync();
                            break;
                        
                        case "Checking":
                            CuentaCorriente cc= _db.CuentaCorrientes.FirstOrDefault(x => x.Id == IdAccount);
                            cc.RequestActive = false;
                            cc.IsActive = true;
                            _db.CuentaCorrientes.Attach(cc);
                            _db.Entry(cc).State = EntityState.Modified; 
                            await _db.SaveChangesAsync();
                            break;
                    }
        
            TempData["mesage"] = "La solitud se ha enviado, para que puedan reactivar su cuenta";
            TempData["status"] = "success";
                var IdBankAccount = Int16.Parse(Request.Cookies["IdBankAccount"]);
                    return new RedirectResult($"/bankaccount/GetAccountProducts/{IdAccount}");
                }

    }
}
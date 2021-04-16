using System.Collections.Generic;
using lab2.Domain.DTOs;
using lab2.Domain.Models;

namespace lab2.Presentation.Account
{
    public class AccountProductsViewModel
    {
        public User user { get; set; }
        public CuentaBancaria cuentaBancaria { get; set; }
        public List<CuentaAhorroDTO> CuentasAhorro { get; set; }
        public List<CuentaCorriente> CuentaCorrientes { get; set; }
        public List<DepositoAPlazoDTO> DepositoAPlazos { get; set; }
        
        public double total { get; set; }
        
        public double totalAccounts { get; set; }
    }
}
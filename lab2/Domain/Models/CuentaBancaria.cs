using System;

namespace lab2.Domain.Models
{
    public class CuentaBancaria
    {
        public int Id { get; set; }
        public Guid NCuenta { get; set; }
        public String Pin { get; set; }
        public int IdUser { get; set; }
    }
}
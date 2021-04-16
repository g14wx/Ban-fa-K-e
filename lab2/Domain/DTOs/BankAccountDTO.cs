using System;

namespace lab2.Domain.DTOs
{
    public class BankAccountDTO
    {
        public String Pin { get; set; }
        public Guid NCuenta { get; set; } 
        public int IdUser { get; set; }
    }
}
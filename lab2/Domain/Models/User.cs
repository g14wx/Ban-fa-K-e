using System;

namespace lab2.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public String Name {get; set; }
        public CuentaBancaria CntBancaria { get; set; }
    }
}
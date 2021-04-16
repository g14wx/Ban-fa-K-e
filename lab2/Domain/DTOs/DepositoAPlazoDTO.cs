using System;

namespace lab2.Domain.DTOs
{
    public class DepositoAPlazoDTO
    {
        public int Id { get; set; }
        public double Cantidad{ get; set; }
        public double TasaInteres{ get; set; }
        public DateTime FechaPlazo { get; set; }
        public DateTime FechaInicio { get; set; }
        public int IdCuentaBancaria { get; set; }
    }
}
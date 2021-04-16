using System;

namespace lab2.Domain.DTOs
{
    public class DepositoAPlazoDTO
    {
        public int Id { get; set; }
        public double Cantidad{ get; set; }
        public double TasaInteres{ get; set; }
        
        public int CantidadDias { get; set; }
        public int IdCuentaBancaria { get; set; }
        
        public double Ganacias { get; set; }
    }
}
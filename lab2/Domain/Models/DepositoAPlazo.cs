using System;

namespace lab2.Domain.Models
{
    public class DepositoAPlazo
    {
        public int Id { get; set; }
        public double Cantidad{ get; set; }
        public double TasaInteres{ get; set; }
        public int CantidadDias { get; set; }
        public int IdCuentaBancaria { get; set; }
    }
}
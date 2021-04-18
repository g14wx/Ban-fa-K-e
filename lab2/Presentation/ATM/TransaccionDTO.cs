using System;

namespace lab2.Presentation.ATM
{
    public class TransaccionDTO
    {
        public double Amount { get; set; }

        public int IdAccount { get; set; }

        public int Tipo { get; set; }//1. abono, 2.retiro

        public double Saldo { get; set; }

        public System.DateTime Fecha { get; set; }

        public String Account { get; set; }
    }
}
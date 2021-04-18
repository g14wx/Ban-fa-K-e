using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab2.Domain.Models
{
    public class Transaccion
    {
        public int Id { get; set; }
        public double Cantidad { get; set; }

        public int IdCuentaAhorro { get; set; }

        public int IdCuentaCorriente { get; set; }

        public int Tipo { get; set; }//0. inicial, 1. abono, 2.retiro

        public double Saldo { get; set; }

        public DateTime Fecha { get; set; }
    }
}

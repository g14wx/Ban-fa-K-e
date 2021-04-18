using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfModels.Models
{
    [Table("Transaccion")]
    public class Transaccion
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("CuentaAhorro")]
        public int IdCuentaAhorro { get; set; }

        [ForeignKey("CuentaCorriente")]
        public int IdCuentaCorriente { get; set; }

        public double Cantidad { get; set; }

        public int Tipo { get; set; }//0. inicial, 1. abono, 2.retiro

        public double Saldo { get; set; }

        public DateTime Fecha { get; set; }
    }
}

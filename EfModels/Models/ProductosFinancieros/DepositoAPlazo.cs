using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfModels.Models.ProductosFinancieros
{
    [Table("DepositoAPlazo")]
    public class DepositoAPlazo
    {
        [Key]
        public int Id { get; set; }
        public double Cantidad{ get; set; }
        public double TasaInteres{ get; set; }
        public DateTime FechaPlazo { get; set; }
        public DateTime FechaInicio { get; set; }
        [ForeignKey("CuentaBancaria")]
        public int IdCuentaBancaria { get; set; }
        public CuentaBancaria CntBancaria { get; set; }
    }
}
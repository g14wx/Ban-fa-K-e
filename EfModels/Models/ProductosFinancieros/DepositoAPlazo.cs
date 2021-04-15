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
        private double Cantidad{ get; set; }
        private double TasaInteres{ get; set; }
        private DateTime FechaPlazo { get; set; }
        private DateTime FechaInicio { get; set; }
        [ForeignKey("CuentaBancaria")]
        public int IdCuentaBancaria { get; set; }
        public CuentaBancaria CntBancaria { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfModels.Models.ProductosFinancieros
{
    [Table("CuentaAhorro")]
    public class CuentaAhorro
    {
        [Key]
        public int Id { get; set; }
        public double Saldo { get; set; }
        public double TasaInteresMensual { get; set; }
        [ForeignKey("CuentaBancaria")]
        public int IdCuentaBancaria { get; set; }
        public CuentaBancaria CntBancaria { get; set; }
    }
}
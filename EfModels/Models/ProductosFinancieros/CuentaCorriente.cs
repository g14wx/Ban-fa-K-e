using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfModels.Models.ProductosFinancieros
{
    [Table("CuentaCorriente")]
    public class CuentaCorriente
    {
        [Key]
        public int Id { get; set; }
        private double Saldo { get; set; }
        [ForeignKey("CuentaBancaria")]
        public int IdCuentaBancaria { get; set; }
        public CuentaBancaria CntBancaria { get; set; }
    }
}
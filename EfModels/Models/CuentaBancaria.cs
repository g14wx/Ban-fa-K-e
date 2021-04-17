using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using EfModels.Models.ProductosFinancieros;

namespace EfModels.Models
{
    [Table("CuentaBancaria")]
    public class CuentaBancaria
    {
        [Key]
        public int Id { get; set; }
        public String NCuenta { get; set; }
        public String Pin { get; set; }
        [ForeignKey("IdUser")] public int IdUser { get; set; }
        public User user { get; set; }
        [JsonIgnore]
        public List<CuentaAhorro> CuentasDeAhorros { get; set; }
        [JsonIgnore]
        public List<CuentaCorriente> CuentaCorrientes { get; set; }
        [JsonIgnore]
        public List<DepositoAPlazo> DepositoAPlazos { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfModels.Models
{
    [Table("Notificacion")]
    public class Notificacion
    {
        [Key]
        public int Id { get; set; }
        public bool IsActive { get; set; } = true;
        public int IdCuenta { get; set; }
        public String ProductoFinanciero { get; set; }
    }
}
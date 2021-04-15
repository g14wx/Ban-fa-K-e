using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EfModels.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        public String Name { get; set; }
        public CuentaBancaria CntBancaria { get; set; } 
    }
}
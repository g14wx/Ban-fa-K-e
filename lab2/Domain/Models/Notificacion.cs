using System;

namespace lab2.Domain.Models
{
    public class notificacion
    {
        public int id { get; set; }
        public bool isactive { get; set; } = true;
        public int idcuenta { get; set; }
        public string productofinanciero { get; set; }    
    }
}
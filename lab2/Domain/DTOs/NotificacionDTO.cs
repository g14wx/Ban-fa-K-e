namespace lab2.Domain.DTOs
{
    public class NotificacionDTO
    {
        public int id { get; set; }
        public bool isactive { get; set; } = true;
        public int idcuenta { get; set; }
        public string productofinanciero { get; set; }       
    }
}
namespace lab2.Domain.Models
{
    public class CuentaCorriente
    {
        public int Id { get; set; }
        public double Saldo { get; set; }
        public int IdCuentaBancaria { get; set; }
        
        public bool IsActive { get; set; }
        
        public bool RequestActive { get; set; }
    }
}
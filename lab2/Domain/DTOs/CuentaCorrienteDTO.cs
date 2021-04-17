namespace lab2.Domain.DTOs
{
    public class CuentaCorrienteDTO
    {
        public int Id { get; set; }
        public double Saldo { get; set; }
        public int IdCuentaBancaria { get; set; }
        
        public bool IsActive { get; set; }
    }
}
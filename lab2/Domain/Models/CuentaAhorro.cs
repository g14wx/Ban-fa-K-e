namespace lab2.Domain.Models
{
    public class CuentaAhorro
    {
        public int Id { get; set; }
        public double Saldo { get; set; }
        public double TasaInteresMensual { get; set; }
        public int IdCuentaBancaria { get; set; }
        
        public int nMeses { get; set; }
        
        public bool IsActive { get; set; }
    }
}
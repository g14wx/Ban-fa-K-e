namespace lab2.Domain.DTOs
{
    public class CuentaAhorroDTO
    {
        public int Id { get; set; }
        public double Saldo { get; set; }
        public double TasaInteresMensual { get; set; }
        public int IdCuentaBancaria { get; set; }    
        public double Ganacias { get; set; }
        
        public int Nmeses  {get;set; }
        
        public bool IsActive { get; set; }
        
        public bool RequestActive { get; set; }
    }
}
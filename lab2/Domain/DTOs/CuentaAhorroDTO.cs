namespace lab2.Domain.DTOs
{
    public class CuentaAhorroDTO
    {
        public int Id { get; set; }
        public double Saldo { get; set; }
        public double TasaInteresMensual { get; set; }
        public int IdCuentaBancaria { get; set; }    }
}
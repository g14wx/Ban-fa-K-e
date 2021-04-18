using EfModels.Models;
using EfModels.Models.ProductosFinancieros;
using Microsoft.EntityFrameworkCore;

namespace EfModels
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
            // Ensure that the database its created 
            Database.EnsureCreated();
        } 
        
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<CuentaBancaria> CuentaBancarias  {get; set; }
        public virtual DbSet<CuentaCorriente> CuentaCorrientes {get; set; }
        public virtual DbSet<CuentaAhorro> CuentaAhorros {get; set; }
        public virtual DbSet<DepositoAPlazo> DepositoAPlazos {get; set; }
        public virtual DbSet<Notificacion> Notificacions { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CuentaBancaria>()
                .HasMany(c => c.CuentasDeAhorros);
            modelBuilder.Entity<CuentaBancaria>()
                .HasMany(c=>c.CuentaCorrientes);
            modelBuilder.Entity<CuentaBancaria>()
                .HasMany(c=>c.DepositoAPlazos);
        }
    }
}
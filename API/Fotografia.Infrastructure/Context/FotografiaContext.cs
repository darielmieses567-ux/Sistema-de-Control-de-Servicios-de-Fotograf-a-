
using Microsoft.EntityFrameworkCore;
using Fotografia.Domain.Entities;

namespace Fotografia.Infrastructure.Context
{
    public class FotografiaContext : DbContext
    {
        public FotografiaContext(
            DbContextOptions<FotografiaContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; } = null!;
        public DbSet<Fotografo> Fotografos { get; set; } = null!;
        public DbSet<Paquete> Paquetes { get; set; } = null!;
        public DbSet<Sesion> Sesiones { get; set; } = null!;
        public DbSet<Pago> Pagos { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Paquete>()
                .Property(p => p.Precio)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Sesion>()
                .Property(s => s.MontoPagado)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Sesion>()
                .Property(s => s.PrecioAcordado)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Pago>()
                .Property(p => p.Monto)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Pago>()
                .HasOne(p => p.Sesion)
                .WithMany(s => s.Pagos)
                .HasForeignKey(p => p.SesionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
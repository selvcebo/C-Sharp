using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TechNova.Models
{
    public partial class TiendaTechNovaDbContext : IdentityDbContext
    {
        public TiendaTechNovaDbContext()
        {
        }

        public TiendaTechNovaDbContext(DbContextOptions<TiendaTechNovaDbContext> options)
            : base(options)
        {
        }

        // DbSets sincronizados con los modelos
        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<Venta> Ventas { get; set; } = null!;
        public virtual DbSet<DetalleVentum> Detalles { get; set; } = null!; // DbSet se llama Detalles

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Usa la cadena de conexión desde appsettings.json
                // optionsBuilder.UseSqlServer("name=DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cliente
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Clientes"); // fuerza nombre de tabla
                entity.HasKey(e => e.ClienteId);
                entity.HasIndex(e => e.CorreoElectronico).IsUnique();
                entity.Property(e => e.CorreoElectronico).HasMaxLength(100);
                entity.Property(e => e.Direccion).HasMaxLength(200);
                entity.Property(e => e.NombreCompleto).HasMaxLength(150);
                entity.Property(e => e.Telefono).HasMaxLength(20);
            });

            // Producto
            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("Productos"); // fuerza nombre de tabla
                entity.HasKey(e => e.ProductoId);
                entity.Property(e => e.Nombre).HasMaxLength(100);
                entity.Property(e => e.Descripcion).HasMaxLength(250);
                entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(10, 2)");
            });

            // Venta
            modelBuilder.Entity<Venta>(entity =>
            {
                entity.ToTable("Ventas"); // 🔎 fuerza nombre de tabla

                entity.HasKey(e => e.VentaId);
                entity.Property(e => e.FechaVenta)
                      .HasDefaultValueSql("(getdate())")
                      .HasColumnType("datetime");
                entity.Property(e => e.TotalVenta).HasColumnType("decimal(12, 2)");

                entity.HasOne(v => v.Cliente)
                      .WithMany(c => c.Ventas)
                      .HasForeignKey(v => v.ClienteId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });

            // DetalleVentum
            modelBuilder.Entity<DetalleVentum>(entity =>
            {
                entity.ToTable("DetalleVenta"); // 🔎 fuerza nombre de la tabla en SQL

                entity.HasKey(e => e.DetalleId);
                entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(10, 2)");
                entity.Property(e => e.Subtotal).HasColumnType("decimal(12, 2)");

                entity.HasOne(d => d.Producto)
                      .WithMany(p => p.Detalles)
                      .HasForeignKey(d => d.ProductoId)
                      .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Venta)
                      .WithMany(v => v.Detalles)
                      .HasForeignKey(d => d.VentaId)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });
        }
    }
}

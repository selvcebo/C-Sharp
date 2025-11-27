using CRUD_TRABAJO.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD_TRABAJO.Data
{
 public class ApplicationDbContext : DbContext
 {
  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
      : base(options)
  {
  }

  public DbSet<Producto> Productos { get; set; }
  public DbSet<Cliente> Clientes { get; set; }
  public DbSet<Venta> Ventas { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
   base.OnModelCreating(modelBuilder);

 
   modelBuilder.Entity<Venta>()
       .HasOne(v => v.Cliente)
       .WithMany(c => c.Ventas)
       .HasForeignKey(v => v.ClienteId)
       .OnDelete(DeleteBehavior.Cascade);

   modelBuilder.Entity<Venta>()
       .HasOne(v => v.Producto)
       .WithMany(p => p.Ventas)
       .HasForeignKey(v => v.ProductoId)
       .OnDelete(DeleteBehavior.Restrict);
  }
 }
}

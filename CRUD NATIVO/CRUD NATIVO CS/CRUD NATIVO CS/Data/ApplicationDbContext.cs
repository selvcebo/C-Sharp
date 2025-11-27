using Microsoft.EntityFrameworkCore;

namespace CRUD_NATIVO_CS.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Models.Producto> Productos { get; set; }
    }
}

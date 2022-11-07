using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ApiProducto.Entidades;
using Microsoft.EntityFrameworkCore;

namespace ApiProducto
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductoInventario>()
                .HasKey(pr => new{ pr.ProductoId, pr.InventarioId });
            }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Inventario> Inventarios { get; set; }

        public DbSet<Marcas> Marcas { get; set; }

        public DbSet<ProductoInventario> ProductoInventario { get; set; }

    }
}

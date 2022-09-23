using ApiProducto.Entidades;
using Microsoft.EntityFrameworkCore;

namespace ApiProducto
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Producto> Productos { get; set; }

        public DbSet<Inventario> Inventario { get; set; }
    }
}
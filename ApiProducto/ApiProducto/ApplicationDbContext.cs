using ApiProducto.Entidades;
using ApiProducto.Migrations;
using Microsoft.EntityFrameworkCore;
using Inventario = ApiProducto.Entidades.Inventario;

namespace ApiProducto
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
    }
        public DbSet<Producto> Productos { get; set; }

        public DbSet<Inventario> Inventario { get; set; }
    }
}
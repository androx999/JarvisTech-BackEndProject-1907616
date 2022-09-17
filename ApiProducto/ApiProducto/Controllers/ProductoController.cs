using ApiProducto.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiProducto.Controllers
{
    [ApiController]
    [Route("producto")]
    public class ProductoController: ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public ProductoController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Producto>>> Get()
        {
            return await dbContext.Productos.ToListAsync();
           
        }

        [HttpPost]
        public async Task<ActionResult> Post(Producto producto) 
        {
            dbContext.Add(producto);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}

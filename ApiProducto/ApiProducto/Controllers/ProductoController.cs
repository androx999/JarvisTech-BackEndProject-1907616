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
        //POST
        [HttpPost]
        public async Task<ActionResult> Post(Producto producto) 
        {
            dbContext.Add(producto);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
        //PUT
        [HttpPut("{id:int}")]

        public async Task<ActionResult> Put(Producto producto,int id) 
        {
           if(producto.Id != id)
            {
                return BadRequest("El id de producto no coincide con el establecido de la url");
            }

            dbContext.Update(producto);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
        //DELETE
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist =await dbContext.Productos.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound();
            }
            dbContext.Remove(new Producto()
            {
                Id = id
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }

    }
}

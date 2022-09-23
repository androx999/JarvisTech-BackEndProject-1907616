using ApiProducto.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiProducto.Controllers
{
    [ApiController]
    [Route("inventario")]
    public class InventarioController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public InventarioController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]

        public async Task<ActionResult<List<Inventario>>> GetAll()
        {
            return await dbContext.Inventario.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Inventario>> GetById(int id)
        {
            return await dbContext.Inventario.FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]

        public async Task<ActionResult> Post(Inventario inventario)
        {
            var productoexiste = await dbContext.Productos.AnyAsync(x => x.Id == inventario.ProductoId);

            if (!productoexiste)
            {
                return BadRequest($"No existe el producto con el id: {inventario.ProductoId}");
            }

            dbContext.Add(inventario);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Inventario inventario, int id)
        {
            var exist = await dbContext.Inventario.AnyAsync(x => x.Id == id);

            if (!exist) 
            {
            return NotFound("La clase especificada no existe");
            }

            if(inventario.Id != id)
            {
                return BadRequest("El id de la clase no coincide con el establecido ");
            }

            dbContext.Update(inventario);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Inventario.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("El recurso no fue encontrado");
            }

            dbContext.Remove(new Inventario { Id = id });
            await dbContext.SaveChangesAsync();
            return Ok();
        }
        
    }
}


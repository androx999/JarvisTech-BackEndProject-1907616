using ApiProducto.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Inventario = ApiProducto.Entidades.Inventario;

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

        public async Task<ActionResult> Post(Inventario y)
        {
            var productoexiste = await dbContext.Productos.AnyAsync(x => x.Id == y.ProductoId);

            if (!productoexiste)
            {
                return BadRequest($"No existe el producto con el id: {y.ProductoId}");
            }

            dbContext.Add(y);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Inventario y, int id)
        {
            var exist = await dbContext.Inventario.AnyAsync(x => x.Id == id);

            if (!exist) 
            {
            return NotFound("La clase especificada no existe");
            }

            if(y.Id != id)
            {
                return BadRequest("El id de la clase no coincide con el establecido ");
            }

            dbContext.Update(y);
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


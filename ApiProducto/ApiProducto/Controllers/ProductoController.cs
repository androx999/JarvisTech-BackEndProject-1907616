using ApiProducto.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiProducto.Controllers
{
    [ApiController]
    [Route("api/producto")]
    public class ProductoController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public ProductoController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        //GET
        [HttpGet]
        [HttpGet("listado")]
        [HttpGet("/listado")]

        public async Task<ActionResult<List<Producto>>> Get()
        {
            return await dbContext.Productos.ToListAsync();

        }

        [HttpGet("primero")]
        public async Task<ActionResult<Producto>> PrimerProducto([FromRoute] int valor, [FromQuery] string producto)
        {
            return await dbContext.Productos.FirstOrDefaultAsync();
        }

        [HttpGet("primero2")]
        public ActionResult<Producto> PrimerProductoD()
        {
            return new Producto() { NameProduct = "DOS" };
        }

        [HttpGet("{id:int}/{param}")]
        public async Task<ActionResult<Producto>> Get(int id, string param)
        {
            var producto = await dbContext.Productos.FirstOrDefaultAsync(x => x.Id == id);

            if (producto == null) {
                return NotFound();
            }
            return Ok(producto);
        }

        [HttpGet("{nombre}")]
        public async Task<ActionResult<Producto>> Get([FromRoute] string nombre)
        { 
            var producto = await dbContext.Productos.FirstOrDefaultAsync(x => x.NameProduct.Contains(nombre));

            if (producto == null)
            {
                return NotFound();
            }
            return Ok(producto);
        }


        //POST
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Producto producto ) 
        {
            dbContext.Add(producto);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
        //PUT
        [HttpPut("{id:int}")]

        public async Task<ActionResult> Put(Producto producto,int id) 
        {
            var exist = await dbContext.Productos.AnyAsync(x => x.Id == id);
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

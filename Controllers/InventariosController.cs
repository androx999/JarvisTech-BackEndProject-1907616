using ApiProducto.Entidades;
using ApiProducto.Migrations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiProducto.Controllers
{

    [ApiController]
    [Route("api/inventarios")]//Ruta del controlador
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class InventariosController: ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<InventariosController> log;
        public InventariosController (ApplicationDbContext context, ILogger<InventariosController> log)
        {
            this.dbContext = context;
            this.log = log;
        }

        [HttpGet]
        [HttpGet("/ordenInventario")]
        public async Task<ActionResult<List<Inventario>>> GetAll() 
        {
            log.LogInformation("Obteniendo orden de inventarios");
            return await dbContext.Inventarios.ToListAsync();
        }

        [HttpGet("{id:int}")]

        public async Task<ActionResult<Inventario>> GetById(int id)
        {
            log.LogInformation("El SN es: " + id);
            return await dbContext.Inventarios.FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Inventario inventario)
        {
            var existeProducto = await dbContext.Inventarios.AnyAsync(x => x.Id == inventario.SerialN);

            if(!existeProducto)
            {
                return BadRequest($"No existe el producto con el numero de serie: {inventario.SerialN} ");
            }

            dbContext.Add(inventario);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]

        public async Task<ActionResult> Put(Inventario inventario, int id)
        {
            var exist = await dbContext.Inventarios.AnyAsync(x => x.Id == id);

            if (!exist)
            {
                return NotFound("No existe dicho inventario");
            }

            if (inventario.Id != id) 
            {
                return BadRequest("El id del producto no ha sido encontrado por lo que no lo tenemos");
            }

            dbContext.Update(inventario);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Inventarios.AnyAsync(x => x.Id == id);

            if (!exist)
            {
                return NotFound("El producto no fue encontrado");
            }

            dbContext.Remove(new Inventario {Id = id});
            await dbContext.SaveChangesAsync();
            return Ok();
        }

    }
}

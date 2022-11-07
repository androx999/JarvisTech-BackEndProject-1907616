using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiProducto.DTOs;
using ApiProducto.Entidades;
using ApiProducto.Migrations;

namespace ApiProducto.Controllers
{

    [ApiController]
    [Route("inventarios")]//Ruta del controlador
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class InventariosController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        public InventariosController(ApplicationDbContext context, IMapper mapper)
        {
            this.dbContext = context;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpGet("/ordenInventario")]
        public async Task<ActionResult<List<Inventario>>> GetAll()
        {
            return await dbContext.Inventarios.ToListAsync();
        }

        [HttpGet("{id:int}", Name = "obtenerInventario")]

        public async Task<ActionResult<InventarioDTOConProductos>> GetById(int id)
        {
            var inventario = await dbContext.Inventarios
                .Include(inventarioDB => inventarioDB.ProductoInventario)
                .ThenInclude(productoInventarioDB => productoInventarioDB.Producto)
                .Include(MarcaDB => MarcaDB.Marcas)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (inventario == null)
            {
                return NotFound();
            }

            inventario.ProductoInventario = inventario.ProductoInventario.OrderBy(x => x.Orden).ToList();

            return mapper.Map<InventarioDTOConProductos>(inventario);
        }

        [HttpPost]
        public async Task<ActionResult> Post(InventarioCreacionDTO inventarioCreacionDTO)
        {
            if (inventarioCreacionDTO.ProductosIds == null)
            {
                return BadRequest("No se puede crear un inventario sin productos");
            }

            var productosIds = await dbContext.Productos
                .Where(productoBD => inventarioCreacionDTO.ProductosIds.Contains(productoBD.Id)).Select(x => x.Id).ToListAsync();

            if (inventarioCreacionDTO.ProductosIds.Count != productosIds.Count)
            {
                return BadRequest("No existe uno de los productos enviados");
            }

            var inventario = mapper.Map<Inventario>(inventarioCreacionDTO);

            OrdenarPorProductos(inventario);

            dbContext.Add(inventario);
            await dbContext.SaveChangesAsync();

            var inventarioDTO = mapper.Map<InventarioDTO>(inventario);

            return CreatedAtRoute("obtenerInventario", new { id = inventario.Id }, inventarioDTO);
        }

        private void OrdenarPorProductos(Inventario inventario)
        {
            if (inventario.ProductoInventario != null)
            {
                for (int i = 0; i < inventario.ProductoInventario.Count; i++)
                {
                    inventario.ProductoInventario[i].Orden = i;
                }
            }
        }

        [HttpPut("{id:int}")]

        public async Task<ActionResult> Put(int id, InventarioCreacionDTO inventarioCreacionDTO)
        {
            var inventarioDB = await dbContext.Inventarios
                .Include(x => x.ProductoInventario)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (inventarioDB == null)
            {
                return NotFound();
            }

            inventarioDB = mapper.Map(inventarioCreacionDTO, inventarioDB);

            OrdenarPorProductos(inventarioDB);

            await dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Inventarios.AnyAsync(x => x.Id == id);

            if (!exist)
            {
                return NotFound("El producto no fue encontrado");
            }

            dbContext.Remove(new Inventario { Id = id });
            await dbContext.SaveChangesAsync();
            return Ok();
        }


        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch(int id, JsonPatchDocument<InventarioPatchDTO> patchDocument)
        {
            if (patchDocument == null) { return BadRequest(); }

            var inventarioDB = await dbContext.Inventarios.FirstOrDefaultAsync(x => x.Id == id);

            if (inventarioDB == null) { return NotFound(); }

            var inventarioDTO = mapper.Map<InventarioPatchDTO>(inventarioDB);

            patchDocument.ApplyTo(inventarioDTO);

            var isValid = TryValidateModel(inventarioDTO);

            if (!isValid)
            {
                return BadRequest(ModelState);
            }

            mapper.Map(inventarioDTO, inventarioDB);

            await dbContext.SaveChangesAsync();
            return NoContent();
        }

    }
}

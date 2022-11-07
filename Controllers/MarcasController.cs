using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.EntityFrameworkCore;
using ApiProducto.DTOs;
using ApiProducto.Entidades;
using System.Text.RegularExpressions;
using ApiProducto.Migrations;

namespace ApiProducto.Controllers
{
    [ApiController]
    [Route("inventarios/{inventarioId:int}/marcas")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MarcasController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;
    

    public MarcasController(ApplicationDbContext dbContext, IMapper mapper,
            UserManager<IdentityUser> userManager)
    {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.userManager = userManager;
    }
        [HttpGet]
        public async Task<ActionResult<List<InventarioDTO>>> Get(int inventarioId)
        {
            var existeInventario = await dbContext.Inventarios.AnyAsync(inventarioDB => inventarioDB.Id == inventarioId);
            if (!existeInventario)
            {
                return NotFound();
            }

            var marcas = await dbContext.Marcas.Where(marcaDB => marcaDB.InventarioId == inventarioId).ToListAsync();

            return mapper.Map<List<InventarioDTO>>(marcas);
        }

        [HttpGet("{id:int}", Name = "obtenerMarca")]
        public async Task<ActionResult<MarcaDTO>> GetById(int id)
        {
            var marca = await dbContext.Marcas.FirstOrDefaultAsync(marcaDB => marcaDB.Id == id);

            if (marca == null)
            {
                return NotFound();
            }

            return mapper.Map<MarcaDTO>(marca);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Post(int inventarioId, MarcaCreacionDTO marcaCreacionDTO)
        {
           
            var existeInventario = await dbContext.Inventarios.AnyAsync(inventarioDB => inventarioDB.Id == inventarioId);
            if (!existeInventario)
            {
                return NotFound();
            }

            var inventario = mapper.Map<Marcas>(marcaCreacionDTO);
            inventario.InventarioId = inventarioId;
            dbContext.Add(inventario);
            await dbContext.SaveChangesAsync();

            var marcaDTO = mapper.Map<InventarioDTO>(inventario);

            return CreatedAtRoute("obtenerCurso", new { id = inventario.Id, inventarioId = inventarioId }, marcaDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int inventarioId, int id, MarcaCreacionDTO marcaCreacionDTO)
        {

            var existeInventario = await dbContext.Inventarios.AnyAsync(marcasDB => marcasDB.Id == id);
            if (!existeInventario)
            {
                return NotFound();
            }

            var marca = mapper.Map<Marcas>(marcaCreacionDTO);
            marca.Id = id;
            marca.InventarioId = inventarioId;

            dbContext.Update(marca);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}

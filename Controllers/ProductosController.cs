using ApiProducto.Entidades;
using ApiProducto.Filtros;
using ApiProducto.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiProducto.Controllers
{

    [ApiController]
    [Route("api/productos")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductosController:ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IService service;
        private readonly ServiceTransient serviceTransient;
        private readonly ServiceScoped serviceScoped;
        private readonly ServiceSingleton serviceSingleton;
        private readonly ILogger<ProductosController> logger;
        private readonly IWebHostEnvironment env;
        private readonly string productosConsultados = "productosConsultados.txt";

        public ProductosController(ApplicationDbContext context, IService service,
            ServiceTransient serviceTransient, ServiceScoped serviceScoped, ServiceSingleton serviceSingleton,
            ILogger<ProductosController> logger, IWebHostEnvironment env)
        {
            this.dbContext = context;
            this.service = service;
            this.serviceTransient = serviceTransient;
            this.serviceScoped = serviceScoped;
            this.serviceSingleton = serviceSingleton;
            this.logger = logger;
            this.env = env;
        }

        [HttpGet("GUID")]
        [ResponseCache(Duration = 10)]
        [ServiceFilter(typeof(FiltroDeAccion))]
        public ActionResult ObtenerGuid()
        {
            throw new NotImplementedException();
            logger.LogInformation("Durante la ejecución");
            return Ok(new
            {
                ProductosControllerTransient = serviceTransient.guid,
                ServiceA_Transient = service.GetTransient(),
                ProductosControllerScoped = serviceScoped.guid,
                ServiceA_Scoped = service.GetScoped(),
                ProductosControllerSingleton = serviceSingleton.guid,
                ServiceA_Singleton = service.GetSingleton()
            });
        }

        [HttpGet] //api/producto
        [HttpGet("orden")]//api/producto/orden
        [HttpGet("/orden")]
        [Authorize]
        public async Task<ActionResult<List<Producto>>> GetProductos()
        {
            throw new NotImplementedException();
            logger.LogInformation("Se obtiene el listado de productos");
            logger.LogWarning("Mensaje de prueba warning");
            service.EjecutarJob();
            return await dbContext.Productos.Include(x => x.inventarios).ToListAsync();
        }

        [HttpGet("primero")]//api/productos/primero
        public async Task<ActionResult<Producto>> PrimerProducto() 
        {
            return await dbContext.Productos.FirstOrDefaultAsync();
        }

        [HttpGet("primero2")]//api/productos/primero
        public ActionResult<Producto> PrimerProductoD()
        {
            return new Producto() { Name = "Play Station"};
        }

        [HttpGet("{param?}")]
        public async Task<ActionResult<Producto>> Get(int id, string param)
        {
           var producto = await dbContext.Productos.FirstOrDefaultAsync(x => x.Id == id);

            if (producto == null)
            {
                return NotFound();
            }
            return producto;
        }

        [HttpGet("obtenerProducto/{nombre}")]
        public async Task<ActionResult<Producto>> Get([FromRoute] string nombre)
        {
            var producto = await dbContext.Productos.FirstOrDefaultAsync(x => x.Name.Contains(nombre));

            if (producto == null)
            {
                logger.LogError("No se encuentra el producto. ");
                return NotFound();
            }
            var ruta = $@"{env.ContentRootPath}\wwwroot\{productosConsultados}";
            using (StreamWriter writer = new StreamWriter(ruta, append: true)) { writer.WriteLine(producto.Id + " " + producto.Name); }

            return producto;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Producto producto)
        {
            var existeProductoMismoNombre = await dbContext.Productos.AnyAsync(x => x.Name == producto.Name);

            if (existeProductoMismoNombre)
            {
                return BadRequest("Ya existe un producto con ese nombre");
            }


            dbContext.Add(producto);
            await dbContext.SaveChangesAsync();
            return Ok(producto);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Producto producto, int id)
        {
            var exist = await dbContext.Productos.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound();
            }

            if (producto.Id != id)
            {
                return BadRequest("El id del alumno no coincide con el establecido en la url");
            }

            dbContext.Update(producto);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Productos.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("No esta disponible");
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

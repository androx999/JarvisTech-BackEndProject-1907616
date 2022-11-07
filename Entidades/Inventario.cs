using ApiProducto.Validaciones;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiProducto.Entidades
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class Inventario
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 250, ErrorMessage = "El campo {0} solo puede tener hasta 250 caracteres")]
        [PrimeraLetraMayuscula]
        public string Name { get; set; }

        public DateTime? FechaRegistro { get; set; }
        

        public List<Marcas>? Marcas { get; set; }
        public List<ProductoInventario> ProductoInventario { get; set; }

    }
}

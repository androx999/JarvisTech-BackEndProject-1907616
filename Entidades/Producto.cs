using ApiProducto.Validaciones;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiProducto.Entidades
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class Producto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 30, ErrorMessage = "El campo {0} solo puede tener hasta 30 caracteres")]
        [PrimeraLetraMayuscula]
        public string Name { get; set; }

        [NotMapped]
        public string Company { get; set; }

        [NotMapped]
        public int Price { get; set; }

        public List<ProductoInventario>? ProductoInventario { get; set; }
    }
}
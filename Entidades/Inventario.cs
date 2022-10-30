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


        [PrimeraLetraMayuscula]

        public string Name { get; set; }

        public string Category { get; set; }

        public int SerialN { get; set; }

        
        public Producto? Producto { get; set; }

    }
}

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

        public List<Inventario>? inventarios { get; set; }

        [NotMapped]
        public int Menor { get; set; }

        [NotMapped]
        public int Mayor { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                var primeraLetra = Name[0].ToString();

                if (primeraLetra != primeraLetra.ToUpper())
                {
                    yield return new ValidationResult("La primera letra debe ser mayuscula",
                        new String[] { nameof(Name) });
                }
            }

            if (Menor > Mayor)
            {
                yield return new ValidationResult("Este valor no puede ser mas grande que el campo Mayor",
                    new String[] { nameof(Menor) });
            }
        }
    }
}
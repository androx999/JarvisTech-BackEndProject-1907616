using System.ComponentModel.DataAnnotations;

namespace ApiProducto.DTOs
{
    public class EditarAdminDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}

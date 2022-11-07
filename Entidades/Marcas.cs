using Microsoft.AspNetCore.Identity;

namespace ApiProducto.Entidades
{
    public class Marcas
    {
        public int Id { get; set; }
        public string CompanyNo { get; set; }

        public int InventarioId { get; set; }

        public Inventario Inventario { get; set; }


    }
}

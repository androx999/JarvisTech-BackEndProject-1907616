namespace ApiProducto.DTOs
{
    public class ProductoDTOConInventarios: GetProductoDTO
    {
        public List<InventarioDTO> InventarioDTOs { get; set; }
    }
}

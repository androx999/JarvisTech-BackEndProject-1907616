namespace ApiProducto.DTOs
{
    public class InventarioDTOConProductos: InventarioDTO
    {

        public List<GetProductoDTO> Productos { get; set; }
    }
}

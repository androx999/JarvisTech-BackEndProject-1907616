namespace ApiProducto.DTOs
{
    public class InventarioDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime FechaRecibo { get; set; }

        public List<MarcaDTO> Marcas { get; set; }
    }
}

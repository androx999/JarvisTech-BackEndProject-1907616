namespace ApiProducto.Entidades
{
    public class Inventario
    {
        public int Id { get; set; }

        public string NameProduct { get; set; }

        public int Price { get; set; }

        public string Category { get; set; }

        public int ProductoId { get; set; }

        public Producto Producto { get; set; }

        public List<Inventario> inventario { get; set; }
     }
}

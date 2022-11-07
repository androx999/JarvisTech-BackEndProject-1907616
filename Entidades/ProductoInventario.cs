namespace ApiProducto.Entidades
{
    public class ProductoInventario
    {
        public int ProductoId { get; set; }

        public string InventarioId { get; set; }

        public string Orden { get; set; }

        public Producto? Producto { get; set; }

        public Inventario? Inventario { get; set; }
    }
}

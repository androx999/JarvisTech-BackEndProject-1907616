namespace ApiProducto.Entidades
{
    public class Producto
    {
        internal object inventario;

        public int Id { get; set; }
        public int Price { get; set; }

        public string NameProduct { get; set; }
    }
}

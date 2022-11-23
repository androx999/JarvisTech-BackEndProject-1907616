using ApiProducto.DTOs;
using ApiProducto.Entidades;
using AutoMapper;

namespace ApiProducto.Utilidades
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ProductoDTO, Producto>();
            CreateMap<Producto, GetProductoDTO>();
            CreateMap<Producto, ProductoDTOConInventarios>()
                .ForMember(ProductoDTO => ProductoDTO.InventarioDTOs, opciones => opciones.MapFrom(MapProductoDTOInventario));
            CreateMap<InventarioCreacionDTO, Inventario>()
                .ForMember(inventario => inventario.ProductoInventario, opciones => opciones.MapFrom(MapProductoInventario));
            CreateMap<Inventario, InventarioDTO>();
            CreateMap<Inventario, InventarioDTOConProductos>()
            .ForMember(inventarioDTO => inventarioDTO.Productos, opciones =>
            {
                opciones.MapFrom(MapInventarioDTOProductos);
            });
            CreateMap<InventarioPatchDTO, Inventario>().ReverseMap();
            CreateMap<MarcaCreacionDTO, Marcas>();
            CreateMap<Marcas, MarcaDTO>();
        }


        private List<InventarioDTO> MapProductoDTOInventario(Producto producto, GetProductoDTO getProductoDTO) 
        {
            var result = new List<InventarioDTO>();

            if (producto.ProductoInvetario == null) { return result; }

            foreach(var productoInvetario in producto.ProductoInventario)
            {
                result.Add(new InventarioDTO()
                {
                    Id = productoInvetario.ProductoId,
                    Name = productoInvetario.ProductoName
                });
            }
            return result;
        }

        private List<GetProductoDTO> MapInventarioDTOProductos(Inventario inventarioDTO) 
        { 
            var result = new List<GetProductoDTO>();

            if(Inventario.ProductoInventario == null) 
            {
                return result;
            }

            foreach (var productoinventario in Inventario.ProductoInventario) 
            {
                result.Add(new GetProductoDTO()
                {
                    SerialN = productoInvetario.SerialN,
                    Name = productoInvetario.Name
                });
            }
            return result;
        }

        private List<ProductoInventario> MapProductoInventario(InventarioCreacionDTO inventarioCreacionDTO, Inventario inventario)
        {
            var resultado = new List<ProductoInventario>();

            if (inventarioCreacionDTO.ProductosIDs == null) { return resultado; }
            foreach (var productoId in inventarioCreacionDTO.ProductsIds)
            {
                resultado.Add(new ProductoClase() { ProductoId = productoId });
            }
            return resultado;
        }
    }
}

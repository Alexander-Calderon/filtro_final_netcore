
using API.Dtos;
using AutoMapper;
using AutoMapper.Configuration;
using Domain.Entities;
namespace API.Profiles;
public class MappingProfile : Profile
{
    public MappingProfile(){


        CreateMap<Cliente, ClienteDto>()
        .ReverseMap();

        CreateMap<DetallePedido, DetallePedidoDto>()        
        .ReverseMap();

        // .ForMember(dest => dest.CodigoPedido, opt => opt.MapFrom(src => src.id))

        CreateMap<Empleado, EmpleadoDto>().ReverseMap();
        CreateMap<GamaProducto, GamaProductoDto>().ReverseMap();
        CreateMap<Oficina, OficinaDto>().ReverseMap();
        CreateMap<Pago, PagoDto>().ReverseMap();

        CreateMap<Pedido, PedidoDto>()        
        .ReverseMap();

        CreateMap<Producto, ProductoDto>().ReverseMap();
        
    }
}

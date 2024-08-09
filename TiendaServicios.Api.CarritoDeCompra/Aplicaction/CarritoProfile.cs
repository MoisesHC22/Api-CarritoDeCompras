using AutoMapper;
using TiendaServicios.Api.CarritoDeCompra.Modelo;

namespace TiendaServicios.Api.CarritoDeCompra.Aplicaction
{
    public class CarritoProfile : Profile 
    {
        public CarritoProfile() 
        {
            CreateMap<CarritoDto, CarritoSesion>().ReverseMap();
            CreateMap<CarritoDetalleDdto, CarritoSesionDetalle>().ReverseMap();
        }
    }
}

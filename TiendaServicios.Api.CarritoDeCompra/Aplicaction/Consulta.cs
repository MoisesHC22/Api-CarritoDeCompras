using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using TiendaServicios.Api.CarritoDeCompra.Persistencia;
using TiendaServicios.Api.CarritoDeCompra.RemoteInterface;

namespace TiendaServicios.Api.CarritoDeCompra.Aplicaction
{
    public class Consulta
    {
        public class Ejecuta : IRequest<CarritoDto> {
        public int CarritoSessionId { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta, CarritoDto> 
        {
            private readonly CarritoContexto carritoContexto;
            private readonly ILibroService libroService;

            public Manejador(CarritoContexto _carritoContexto, ILibroService _libroService)
            {
                carritoContexto = _carritoContexto;
                libroService = _libroService;
            }

            public async Task<CarritoDto> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                //Ontenemos el carrito almacenado en la base de datos pasando el 
                var carritoSesion = await carritoContexto.CarritoSesiones.FirstOrDefaultAsync(x => x.CarritoSesionId ==
                    request.CarritoSessionId);
                //Devuelce la lista de producto detalle solo para conocer el
                var carritoSessionDetalle = await carritoContexto.
                    CarritoSesionDetalle.Where(x => x.CarritoSesionId ==
                    request.CarritoSessionId).ToListAsync();

                var listaCarritoDto = new List<CarritoDetalleDdto>();

                foreach (var libro in carritoSessionDetalle)
                {
                    //Invocamos a la microservice externa
                    var response = await libroService.
                        GetLibro(new System.Guid(libro.ProductoSeleccionado));
                    if (response.resultado)
                    {
                        //Se accede si se encuentra algo en la base datos
                        var objectoLibro = response.Libro; // Retorno un libroRemove
                        var carritoDetalle = new CarritoDetalleDdto
                        {
                            TituloLibro = objectoLibro.Titulo,
                            FechaPublicacion = objectoLibro.FechaPublicacion,
                            LibroId = objectoLibro.LibreriaMateriaId
                        };
                        listaCarritoDto.Add(carritoDetalle);
                    }
                }
                // Llenamos el objet que realmente es necesario retornar
                var carritoSessionDto = new CarritoDto
                {
                    CarritoId = carritoSesion.CarritoSesionId,
                    FechaCreacionSesion = carritoSesion.FechaCreacion,
                    LlistaDeProductos = listaCarritoDto
                };
                return carritoSessionDto;
            }




        }
    }
}

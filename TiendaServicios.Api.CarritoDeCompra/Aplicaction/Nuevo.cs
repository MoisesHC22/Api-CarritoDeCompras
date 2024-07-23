using MediatR;
using System.Reflection.Metadata.Ecma335;
using TiendaServicios.Api.CarritoDeCompra.Modelo;
using TiendaServicios.Api.CarritoDeCompra.Persistencia;

namespace TiendaServicios.Api.CarritoDeCompra.Aplicaction
{
    public class Nuevo
    {
        public class Ejecuta : IRequest {
            public DateTime FechaCreacionSesion {  get; set; }
            public List<string> ProductoLista { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly CarritoContexto _contexto;
            public Manejador(CarritoContexto contexto)
            {
                _contexto = contexto;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritoSesion = new CarritoSesion
                {
                    FechaCreacion = request.FechaCreacionSesion
                };
                _contexto.CarritoSesiones.Add(carritoSesion);

                var result = await _contexto.SaveChangesAsync();
                if (result == 0)
                {
                    throw new Exception("No se pudo insertar en el Carrito de compras");
                }

                int id = carritoSesion.CarritoSesionId;
                foreach (var p in request.ProductoLista)
                {
                    var detalleSesion = new CarritoSesionDetalle
                    {
                        FechaCreacion = DateTime.Now,
                        CarritoSesionId = id,
                        ProductoSeleccionado = p
                    };

                    _contexto.CarritoSesionDetalle.Add(detalleSesion);
                }
                var value = await _contexto.SaveChangesAsync();

                if (value > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo inserta el detalle del carrido de compras");
            }
            

        }
    }
}

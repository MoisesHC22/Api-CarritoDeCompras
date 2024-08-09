using MediatR;
using System.Reflection.Metadata.Ecma335;
using TiendaServicios.Api.CarritoDeCompra.Modelo;
using TiendaServicios.Api.CarritoDeCompra.Persistencia;
using TiendaServicios.Api.CarritoDeCompra.RemoteInterface;

namespace TiendaServicios.Api.CarritoDeCompra.Aplicaction
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public DateTime FechaCreacionSesion { get; set; }
            public List<ProductoCantidad> ProductoLista { get; set; }
        }

        public class ProductoCantidad
        {
            public string ProductoID { get; set; }
            public int cantidad { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly CarritoContexto _contexto;
            private readonly ILibroService _libroService;
            private readonly ICuponService _cuponService;

            public Manejador(CarritoContexto contexto, ILibroService libroService, ICuponService cuponService)
            {
                _contexto = contexto;
                _libroService = libroService;
                _cuponService = cuponService;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritoSesion = new CarritoSesion
                {
                    FechaCreacion = DateTime.Now,
                };

                _contexto.CarritoSesiones.Add(carritoSesion);
                await _contexto.SaveChangesAsync();

                int id = carritoSesion.CarritoSesionId;

                foreach (var p in request.ProductoLista)
                {
                    var InfProducto = await _libroService.GetLibro(new Guid(p.ProductoID));
                    if (InfProducto.Libro == null)
                    {
                        continue;
                    }

                    var Descuento = 0m;
                    if (InfProducto.Libro.cupon.HasValue) 
                    {
                        var InfDescuento = await _cuponService.GetCupon(InfProducto.Libro.cupon.Value);

                        if (InfDescuento.cupon != null && InfDescuento.cupon.FechaInicio <= DateTime.Now && InfDescuento.cupon.FechaExpiracion >= DateTime.Now)
                        {
                            Descuento = ((decimal)InfDescuento.cupon.PorcetanjeDescuento / 100) * (decimal)InfProducto.Libro.PrecioConIva;
                        }
                    }

                    var detalleSesion = new CarritoSesionDetalle
                    {
                        FechaCreacion = DateTime.Now,
                        CarritoSesionId = id,
                        ProductoSeleccionado = p.ProductoID,
                        Cantidad = p.cantidad,
                        TotalProducto = (decimal)(p.cantidad * (Descuento > 0 ? Descuento : InfProducto.Libro.PrecioConIva))
                    };

                    _contexto.CarritoSesionDetalle.Add(detalleSesion);
                }

                var result = await _contexto.SaveChangesAsync();
                if (result <= 0) 
                {
                    throw new Exception("No se pudo insertar los detalles del carrito de compras.");
                }
                return Unit.Value;

            }
        }




    }
}


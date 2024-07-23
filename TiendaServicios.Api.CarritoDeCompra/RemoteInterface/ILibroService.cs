using TiendaServicios.Api.CarritoDeCompra.RemoteModel;

namespace TiendaServicios.Api.CarritoDeCompra.RemoteInterface
{
    public interface ILibroService
    {
        Task<(bool resultado, LibroRemote Libro, string ErrorMessage)>
            GetLibro(Guid LibroId);
    }
}

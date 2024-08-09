using TiendaServicios.Api.CarritoDeCompra.RemoteModel;

namespace TiendaServicios.Api.CarritoDeCompra.RemoteInterface
{
    public interface IAutorService
    {
        Task<(bool resultado, AutorRemote autor, string ErrorMessage)>
            GetAutor(string autorId);
    }
}

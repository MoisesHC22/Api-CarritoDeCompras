using TiendaServicios.Api.CarritoDeCompra.RemoteModel;

namespace TiendaServicios.Api.CarritoDeCompra.RemoteInterface
{
    public interface ICuponService
    {
        Task<(bool resultado, CuponRemote cupon, string ErrorMessage)> GetCupon(int CuponId);
    }
}

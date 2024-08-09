namespace TiendaServicios.Api.CarritoDeCompra.RemoteModel
{
    public class CuponRemote
    {
        public int CuponId { get; set; }
        public string CuponCode { get; set; }
        public double PorcetanjeDescuento { get; set; }
        public int DescuentoMinimo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaExpiracion { get; set; }
    }
}

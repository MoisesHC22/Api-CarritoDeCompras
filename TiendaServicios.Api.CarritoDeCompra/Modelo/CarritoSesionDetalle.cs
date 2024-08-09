using System.ComponentModel.DataAnnotations;

namespace TiendaServicios.Api.CarritoDeCompra.Modelo
{
    public class CarritoSesionDetalle
    {
        [Key]
        public int CarritoSessionDetalleId { get; set; }
        [Required]
        public DateTime? FechaCreacion { get; set; }
        [Required]
        public string ProductoSeleccionado { get; set; }
        [Required]
        public int Cantidad { get; set; }
        [Required]
        public decimal TotalProducto { get; set; }
        [Required]
        public int CarritoSesionId { get; set; }
        public CarritoSesion carritoSesion { get; set; }
    }
}

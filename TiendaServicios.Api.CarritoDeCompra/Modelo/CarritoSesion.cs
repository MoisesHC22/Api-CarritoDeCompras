using System.ComponentModel.DataAnnotations;

namespace TiendaServicios.Api.CarritoDeCompra.Modelo
{
    public class CarritoSesion
    {
        [Key]
        public int CarritoSesionId { get; set; }
        [Required]
        public DateTime? FechaCreacion { get; set; }
        [Required]
        public ICollection<CarritoSesionDetalle> ListaDetalle { get; set; }
    }
}

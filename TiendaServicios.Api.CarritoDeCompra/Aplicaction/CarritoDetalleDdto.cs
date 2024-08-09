namespace TiendaServicios.Api.CarritoDeCompra.Aplicaction
{
    public class CarritoDetalleDdto
    {
        public Guid? LibroId { get; set; }
        public string TituloLibro { get; set; }
        public string AutorLibro { get; set; }
        public string Imagen {  get; set; }
        public string Autor { get; set; }
        public decimal? Precio { get; set; }

        public int cantidad { get; set; }
        public decimal TotalProducto { get; set; }
    }
}

namespace TiendaServicios.Api.CarritoDeCompra.RemoteModel
{
    public class LibroRemote
    {
       public Guid? LibreriaMaterialId { get; set; }
       public string Titulo {  get; set; }
       public DateTime? FechaPublicacion { get; set;}
       public string AutorLibro { get; set; }
       public string Imagen {  get; set; }
       public decimal? Precio { get; set; }
       public decimal? Iva { get; set; }
       public int? cupon { get; set; }
        public decimal? PrecioConIva { get; set; }
    }
}

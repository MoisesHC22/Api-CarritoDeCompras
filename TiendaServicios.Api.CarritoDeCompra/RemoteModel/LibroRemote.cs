﻿namespace TiendaServicios.Api.CarritoDeCompra.RemoteModel
{
    public class LibroRemote
    {
       public Guid? LibreriaMateriaId { get; set; }
       public string Titulo {  get; set; }
       public DateTime? FechaPublicacion { get; set;}
       public Guid? AutorLibro { get; set; }
    }
}

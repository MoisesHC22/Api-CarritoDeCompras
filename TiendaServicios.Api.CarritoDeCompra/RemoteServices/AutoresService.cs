using TiendaServicios.Api.CarritoDeCompra.RemoteInterface;
using TiendaServicios.Api.CarritoDeCompra.RemoteModel;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text.Json;

namespace TiendaServicios.Api.CarritoDeCompra.RemoteServices
{
    public class AutoresService : IAutorService
    {
        private readonly IHttpClientFactory httpClient;
        private readonly ILogger<AutoresService> logger;

        public AutoresService(IHttpClientFactory httpClient, ILogger<AutoresService> logger)
        {
            this.httpClient = httpClient;
            this.logger = logger;
        }

        public async Task<(bool resultado, AutorRemote autor, string ErrorMessage)> GetAutor(string autorId)
        {
            try
            {
                var cliente = httpClient.CreateClient("Autores");
                var response = await cliente.GetAsync($"/Autor/GetAutorLibro?id={autorId}");
                if (response.IsSuccessStatusCode)
                {
                    var contenido = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var resultado = JsonSerializer.Deserialize<AutorRemote>(contenido, options);
                    return (true, resultado, null);
                }
                return (false, null, response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}

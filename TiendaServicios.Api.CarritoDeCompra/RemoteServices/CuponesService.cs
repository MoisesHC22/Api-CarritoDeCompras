using System.Text.Json;
using TiendaServicios.Api.CarritoDeCompra.RemoteInterface;
using TiendaServicios.Api.CarritoDeCompra.RemoteModel;

namespace TiendaServicios.Api.CarritoDeCompra.RemoteServices
{
    public class CuponesService : ICuponService
    {
        private readonly IHttpClientFactory httpClient;
        private readonly ILogger<AutoresService> logger;

        public CuponesService(IHttpClientFactory httpClient, ILogger<AutoresService> logger)
        {
            this.httpClient = httpClient;
            this.logger = logger;
        }

        public async Task<(bool resultado, CuponRemote cupon, string ErrorMessage)> GetCupon(int CuponId)
        {
            try
            {
                var cliente = httpClient.CreateClient("Cupones");
                var response = await cliente.GetAsync($"/Cupones/GetCupon?id={CuponId}");
                if (response.IsSuccessStatusCode)
                {
                    var contenido = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var resultado = JsonSerializer.Deserialize<CuponRemote>(contenido, options);
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

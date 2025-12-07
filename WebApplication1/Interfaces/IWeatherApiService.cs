// Interfaces/IWeatherApiService.cs

using CatalogoFilmesTempo.Models.Api;
using System.Threading.Tasks;

namespace CatalogoFilmesTempo.Interfaces
{
    // Define o método para buscar a previsão do tempo por coordenadas.
    public interface IWeatherApiService
    {
        // Busca a previsão do tempo usando Latitude e Longitude.
        Task<WeatherForecast?> GetWeatherForecastAsync(double latitude, double longitude);
    }
}
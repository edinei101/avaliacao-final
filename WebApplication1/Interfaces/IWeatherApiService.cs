// Interfaces/IWeatherApiService.cs
using CatalogoFilmesTempo.Models.Weather;
using System.Threading.Tasks;

namespace CatalogoFilmesTempo.Interfaces
{
    public interface IWeatherApiService
    {
        // Assinatura correta com Lat/Lon
        Task<WeatherForecast?> GetWeatherForecastAsync(double latitude, double longitude);
    }
}
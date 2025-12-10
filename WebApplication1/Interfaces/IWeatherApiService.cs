using CatalogoFilmesTempo.Models.Weather;
using System.Threading.Tasks;

namespace CatalogoFilmesTempo.Interfaces
{
    public interface IWeatherApiService
    {
        // RF06/RF07: Busca a previsão do tempo por nome da cidade
        Task<WeatherForecast?> GetWeatherForecastAsync(string cityName);
    }
}
// Services/WeatherServiceCacheDecorator.cs
using CatalogoFilmesTempo.Interfaces;
using CatalogoFilmesTempo.Models.Weather;
using System.Threading.Tasks;
using CatalogoFilmesTempo.Services;

// Adiciona o using para o namespace onde WeatherApiService está definido:
// (Se WeatherApiService estiver no mesmo namespace 'CatalogoFilmesTempo.Services',
// você não precisa deste using. Assumimos que está em 'Services'.)

namespace CatalogoFilmesTempo.Services
{
    // A classe agora pode encontrar WeatherApiService
    public class WeatherServiceCacheDecorator : IWeatherApiService
    {
        // Garante que o serviço real seja do tipo concreto correto
        private readonly WeatherApiService _realService;

        public WeatherServiceCacheDecorator(WeatherApiService realService)
        {
            _realService = realService;
        }

        public async Task<WeatherForecast?> GetWeatherForecastAsync(double latitude, double longitude)
        {
            // O serviço real é chamado com a nova assinatura
            return await _realService.GetWeatherForecastAsync(latitude, longitude);
        }
    }
}
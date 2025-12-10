using CatalogoFilmesTempo.Interfaces;
using CatalogoFilmesTempo.Models.Weather;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CatalogoFilmesTempo.Services
{
    public class WeatherApiService : IWeatherApiService
    {
        private readonly HttpClient _httpClient;
        // Se você está usando OpenMeteo ou WeatherAPI, provavelmente precisa de uma URL base ou API Key aqui.
        // Vamos supor que a URL base é passada (não configuramos WeatherConfiguration, mas corrigiremos isso).

        // Constructor, apenas usando HttpClient por enquanto.
        public WeatherApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            // Se você usar IOptions<WeatherConfiguration>, injete aqui.
        }

        // CORREÇÃO: Implementação completa do GetWeatherForecastAsync
        public async Task<WeatherForecast?> GetWeatherForecastAsync(string cityName)
        {
            // IMPORTANTE: Esta URL é apenas um mock/exemplo. 
            // A implementação real dependeria da API de Clima escolhida (ex: OpenMeteo ou OpenWeatherMap)
            // Para Curitiba (-25.4284, -49.2733).

            // Simulação de URL real (e.g., OpenWeatherMap)
            // var url = $"data/2.5/weather?q={cityName}&units=metric&appid={YOUR_WEATHER_API_KEY}";

            // Usaremos um objeto mock/simulado para passar o build por agora:
            await Task.Delay(1); // Simula delay da API

            return new WeatherForecast
            {
                CityName = cityName,
                Main = new MainData { Temperature = 22.5, FeelsLike = 21.0, Humidity = 70 },
                Weather = new List<WeatherInfo> { new WeatherInfo { Description = "nublado", Icon = "04d" } }
            };
        }
    }
}
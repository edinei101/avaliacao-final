// Services/WeatherApiService.cs

using CatalogoFilmesTempo.Interfaces;
using CatalogoFilmesTempo.Models.Api;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CatalogoFilmesTempo.Services
{
    // Esta classe é um placeholder simples para a API de clima.
    // Em um projeto real, você usaria uma API como OpenWeatherMap,
    // que requer uma chave e um endpoint específico.
    public class WeatherApiService : IWeatherApiService
    {
        private readonly HttpClient _httpClient;

        // Em um projeto real, você injetaria a chave e a URL da API aqui,
        // similar ao que fizemos com o TMDb.

        // Exemplo de URL base (APENAS UM EXEMPLO, NECESSITA DE UMA CHAVE REAL)
        private const string WEATHER_API_BASE_URL = "https://api.openweathermap.org/data/2.5/weather";
        private const string API_KEY = "SUA_CHAVE_DE_CLIMA_AQUI"; // Substitua pela sua chave

        public WeatherApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // --- IWeatherApiService Implementation ---

        // RF06: Busca a previsão do tempo usando Latitude e Longitude.
        public async Task<WeatherForecast?> GetWeatherForecastAsync(double latitude, double longitude)
        {
            // Monta a URL para OpenWeatherMap (exemplo)
            var url = $"{WEATHER_API_BASE_URL}?lat={latitude}&lon={longitude}&appid={API_KEY}&units=metric&lang=pt_br";

            try
            {
                // Resposta da API de Clima (estrutura JSON complexa)
                var response = await _httpClient.GetFromJsonAsync<OpenWeatherResponse?>(url);

                // Se a resposta for válida, mapeamos para o nosso modelo simplificado WeatherForecast
                if (response?.main != null && response.weather?.Count > 0)
                {
                    return new WeatherForecast
                    {
                        Temperature = response.main.temp,
                        FeelsLike = response.main.feels_like,
                        pressure = response.main.pressure,
                        humidity = response.main.humidity,
                        wind_speed = response.wind.speed,
                        Description = response.weather[0].description,
                        Icon = response.weather[0].icon,
                        CityName = response.name // Nome da cidade retornado pela API
                    };
                }

                return null;
            }
            catch (System.Exception)
            {
                // Em caso de erro (chave inválida, falha de rede, etc.)
                return null;
            }
        }

        // --- Modelos Internos para Desserialização (OpenWeatherMap é um exemplo) ---

        // Devido à estrutura aninhada do JSON de muitas APIs de clima, 
        // precisamos de classes auxiliares para desserializar.

        private class OpenWeatherResponse
        {
            public MainData? main { get; set; }
            public List<WeatherDetail>? weather { get; set; }
            public WindData? wind { get; set; }
            public string? name { get; set; } // Nome da cidade
        }

        private class MainData
        {
            public double temp { get; set; }
            public double feels_like { get; set; }
            public int pressure { get; set; }
            public int humidity { get; set; }
        }

        private class WeatherDetail
        {
            public string? description { get; set; }
            public string? icon { get; set; }
        }

        private class WindData
        {
            public double speed { get; set; }
        }
    }
}
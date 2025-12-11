// Services/WeatherApiService.cs

using CatalogoFilmesTempo.Interfaces;
using CatalogoFilmesTempo.Models.Weather;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json; // 🚨 Adicionado: ESSENCIAL para o desserializador
using System; // 🚨 Adicionado: Para o bloco try/catch e Console.WriteLine
using System.Collections.Generic; // 🚨 Adicionado: ESSENCIAL para List<T>

namespace CatalogoFilmesTempo.Services
{
    public class WeatherApiService : IWeatherApiService
    {
        private readonly HttpClient _httpClient;

        // URL base da API Open-Meteo (não requer chave)
        private const string BaseUrl = "https://api.open-meteo.com/v1/forecast";

        public WeatherApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeatherForecast?> GetWeatherForecastAsync(double latitude, double longitude)
        {
            try
            {
                // URL completa para o clima ATUAL. Usamos :F2 para garantir ponto decimal,
                // prevenindo erros de API relacionados à cultura (ex: vírgula).
                string url = $"{BaseUrl}?latitude={latitude:F2}&longitude={longitude:F2}&current=temperature_2m,relative_humidity_2m,weather_code&temperature_unit=celsius&forecast_days=1";

                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                // 🚨 CORREÇÃO CRÍTICA: Desserializar como LISTA e pegar o primeiro item 🚨
                var forecastList = JsonSerializer.Deserialize<List<WeatherForecast>>(
                    content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );

                if (forecastList != null && forecastList.Count > 0)
                {
                    var forecast = forecastList[0];

                    // Atribui as coordenadas para exibição
                    forecast.City = $"Lat: {latitude:F2}, Lon: {longitude:F2}";

                    return forecast;
                }

                // Se a API retornou 200, mas o conteúdo estava vazio/inválido
                return null;
            }
            catch (Exception ex)
            {
                // Em caso de falha de conexão ou erro interno (ex: 4xx, 5xx)
                Console.WriteLine($"Erro ao buscar previsão do tempo: {ex.Message}");
                return null;
            }
        }
    }
}
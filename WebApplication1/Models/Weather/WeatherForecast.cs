// Models/Weather/WeatherForecast.cs

using System.Text.Json.Serialization;
using System.Collections.Generic;
using System;

namespace CatalogoFilmesTempo.Models.Weather
{
    // Modelo Principal que mapeia UM objeto dentro do ARRAY JSON retornado pela API.
    public class WeatherForecast
    {
        // Propriedade para ser preenchida manualmente no Service (não está no JSON)
        public string City { get; set; } = "Localização Indisponível";

        // Mapeamento das coordenadas (estão no nível principal do JSON)
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        // Mapeia o objeto "current" da resposta JSON
        [JsonPropertyName("current")]
        public CurrentWeather? Current { get; set; }

        // Propriedade de conveniência para acessar a temperatura rapidamente
        public double Temperature => Current?.Temperature ?? 0.0;

        // Propriedade de conveniência para acessar a umidade rapidamente
        public int Humidity => Current?.Humidity ?? 0;

        // Se quiser adicionar de volta a lógica de Max/Min no futuro, você pode 
        // mas por enquanto, focamos em Current para resolver o problema imediato.

        // Método utilitário para traduzir o código do tempo (mantido para a View)
        public string GetWeatherSummary()
        {
            var code = Current?.WeatherCode;
            return code switch
            {
                0 => "Céu Limpo",
                1 or 2 or 3 => "Parcialmente Nublado",
                45 or 48 => "Nevoeiro",
                51 or 53 or 55 => "Chuvisco",
                61 or 63 or 65 => "Chuva",
                // ... adicione mais códigos conforme necessário
                _ => "Condição Desconhecida"
            };
        }
    }

    // Classe Aninhada: Mapeia o bloco "current" (Clima Atual)
    public class CurrentWeather
    {
        [JsonPropertyName("temperature_2m")]
        public double Temperature { get; set; }

        [JsonPropertyName("relative_humidity_2m")]
        public int Humidity { get; set; }

        [JsonPropertyName("weather_code")]
        public int WeatherCode { get; set; }
    }
}
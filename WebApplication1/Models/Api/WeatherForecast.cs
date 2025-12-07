// Models/Api/WeatherForecast.cs

using System;
using System.Text.Json.Serialization;

namespace CatalogoFilmesTempo.Models.Api
{
    // Modelo simples para representar a previsão do tempo.
    public class WeatherForecast
    {
        [JsonPropertyName("temp")]
        public double Temperature { get; set; }

        [JsonPropertyName("feels_like")]
        public double FeelsLike { get; set; }

        public int pressure { get; set; }
        public int humidity { get; set; }
        public double wind_speed { get; set; }

        // A descrição do clima (e.g., "clear sky", "rain")
        public string? Description { get; set; }

        // Um ícone de referência (código)
        public string? Icon { get; set; }

        // Adicionamos a Cidade para facilitar a exibição na View
        public string? CityName { get; set; }
    }
}
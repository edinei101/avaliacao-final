using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CatalogoFilmesTempo.Models.Weather
{
    public class WeatherForecast
    {
        public string CityName { get; set; } = "Localização Desconhecida";

        [JsonPropertyName("main")]
        public MainData Main { get; set; } = new MainData(); // <-- DADOS PRINCIPAIS AQUI

        [JsonPropertyName("weather")]
        public List<WeatherInfo> Weather { get; set; } = new List<WeatherInfo>(); // <-- DETALHES DE CLIMA AQUI

        // Adicionar WindData se o Partial View precisar
        // public WindData Wind { get; set; } = new WindData(); 
    }

    public class MainData
    {
        [JsonPropertyName("temp")]
        public double Temperature { get; set; }

        [JsonPropertyName("feels_like")]
        public double FeelsLike { get; set; }

        [JsonPropertyName("humidity")]
        public int Humidity { get; set; }
    }

    // Se precisar de vento, adicione esta classe:
    public class WindData
    {
        [JsonPropertyName("speed")]
        public double Speed { get; set; }
    }

    public class WeatherInfo
    {
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("icon")]
        public string Icon { get; set; } = string.Empty;
    }
}
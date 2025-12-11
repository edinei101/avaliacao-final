// Models/Api/TmdbConfiguration.cs

using System.ComponentModel.DataAnnotations;

namespace CatalogoFilmesTempo.Models.Api
{
    // A constante continua correta, referenciando a seção "TmdbOptions"
    public class TmdbConfiguration
    {
        public const string Tmdb = "TmdbOptions";

        [Required]
        // Esta é a propriedade que está sendo carregada no serviço
        public string ApiKey { get; set; } = string.Empty;

        // BaseUrl usado no TmdbApiService
        [Required]
        public string BaseUrl { get; set; } = "https://api.themoviedb.org/3/";

        // Mapeia BaseImageUrl do appsettings.json
        [Required]
        public string BaseImageUrl { get; set; } = "https://image.tmdb.org/t/p/w500";
    }
}
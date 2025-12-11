// Configurations/TmdbOptions.cs
using System.ComponentModel.DataAnnotations;

namespace CatalogoFilmesTempo.Configurations
{
    // A CLASSE DE CONFIGURAÇÃO USADA NO PROGRAM.CS
    public class TmdbConfigurationOptions
    {
        // Se a classe Models/Api/TmdbConfiguration existe, você deve usar esta classe 
        // apenas para mapeamento e injetar a classe Models/Api/TmdbConfiguration.

        [Required]
        public string ApiKey { get; set; } = string.Empty;

        [Required]
        public string BaseUrl { get; set; } = "https://api.themoviedb.org/3/";

        [Required]
        public string ImageBaseUrl { get; set; } = "https://image.tmdb.org/t/p/w500";
    }
}
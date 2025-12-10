namespace CatalogoFilmesTempo.Models.Api
{
    public class TmdbConfiguration
    {
        public string TmdbImageBaseUrl { get; set; } = string.Empty;

        // CORREÇÃO: Adicionando a chave da API (Bind do appsettings.json)
        public string TmdbApiKey { get; set; } = string.Empty;
    }
}
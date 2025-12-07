// Services/TmdbApiService.cs

using CatalogoFilmesTempo.Interfaces;
using CatalogoFilmesTempo.Models.Api;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CatalogoFilmesTempo.Services
{
    // Implementa a interface ITmdbApiService para buscar dados no TMDb.
    public class TmdbApiService : ITmdbApiService
    {
        private readonly HttpClient _httpClient;
        private readonly TmdbConfiguration _config;

        // Injeção de Dependência: HttpClient e IOptions<TmdbConfiguration>
        public TmdbApiService(
            HttpClient httpClient,
            IOptions<TmdbConfiguration> config)
        {
            _httpClient = httpClient;
            // Acessa as configurações (URL e Chave) de forma segura
            _config = config.Value;
        }

        // --- Método Auxiliar ---

        // Constrói a URL completa da API
        private string GetTmdbUrl(string endpoint, string queryParameters = "")
        {
            // O endpoint deve começar com '/' (ex: "/search/movie")
            return $"{_config.TmdbBaseUrl!.TrimEnd('/')}{endpoint}?api_key={_config.TmdbApiKey}{queryParameters}";
        }

        // --- ITmdbApiService Implementation ---

        // RF02: Busca filmes por termo.
        public async Task<TmdbSearchResponse?> SearchMoviesAsync(string query, int page)
        {
            var endpoint = "/search/movie";
            // Codifica a query para ser segura na URL
            var queryParams = $"&query={System.Net.WebUtility.UrlEncode(query)}&page={page}";

            var url = GetTmdbUrl(endpoint, queryParams);

            try
            {
                // Faz a chamada GET e desserializa o JSON diretamente para o modelo TmdbSearchResponse
                var response = await _httpClient.GetFromJsonAsync<TmdbSearchResponse>(url);
                return response;
            }
            catch (HttpRequestException)
            {
                // Tratar ou logar erros de HTTP
                return null;
            }
            catch (System.Text.Json.JsonException)
            {
                // Tratar erros de desserialização JSON
                return null;
            }
        }

        // RF03: Obtém detalhes de um filme pelo ID.
        public async Task<MovieDetail?> GetMovieDetailAsync(int tmdbId)
        {
            var endpoint = $"/movie/{tmdbId}";
            var url = GetTmdbUrl(endpoint);

            try
            {
                // Faz a chamada GET e desserializa o JSON para o modelo MovieDetail
                var response = await _httpClient.GetFromJsonAsync<MovieDetail>(url);
                return response;
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }
    }
}
using CatalogoFilmesTempo.Interfaces;
using CatalogoFilmesTempo.Models.Api;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CatalogoFilmesTempo.Services
{
    public class TmdbApiService : ITmdbApiService
    {
        private readonly HttpClient _httpClient;
        private readonly TmdbConfiguration _config;

        public TmdbApiService(HttpClient httpClient, IOptions<TmdbConfiguration> configOptions)
        {
            _httpClient = httpClient;
            _config = configOptions.Value;
        }

        // CORREÇÃO: Implementação completa do SearchMoviesAsync
        public async Task<TmdbSearchResponse?> SearchMoviesAsync(string query)
        {
            var url = $"search/movie?api_key={_config.TmdbApiKey}&query={query}";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<TmdbSearchResponse>(content);
            }
            return null;
        }

        // Implementação do GetMovieDetailAsync (Deve existir, mas garantindo a assinatura)
        public async Task<MovieDetail?> GetMovieDetailAsync(int id)
        {
            var url = $"movie/{id}?api_key={_config.TmdbApiKey}";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<MovieDetail>(content);
            }
            return null;
        }
    }
}
// Services/TmdbApiService.cs

// Usings para Interfaces (supondo que IApi está em Interfaces) e Models.Api (sua configuração e DTOs)
using CatalogoFilmesTempo.Interfaces; // Para encontrar ITmdbApiService
using CatalogoFilmesTempo.Models.Api; // Para encontrar TmdbConfiguration, TmdbSearchResponse, MovieDetail
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System;

namespace CatalogoFilmesTempo.Services
{
    // Apenas UMA definição da classe
    public class TmdbApiService : ITmdbApiService
    {
        // Campos privados declarados APENAS UMA VEZ
        private readonly HttpClient _httpClient;
        private readonly TmdbConfiguration _config; // Usa o modelo de configuração de Models/Api

        // Construtor declarado APENAS UMA VEZ
        public TmdbApiService(HttpClient httpClient, IOptions<TmdbConfiguration> config)
        {
            _httpClient = httpClient;
            _config = config.Value;
        }

        // Método SearchMoviesAsync declarado APENAS UMA VEZ
        public async Task<TmdbSearchResponse?> SearchMoviesAsync(string query)
        {



            if (string.IsNullOrEmpty(_config.ApiKey))
            {
                // Se a chave não estiver configurada, retorna nulo.
                return null;
            }

            var url = $"search/movie?query={Uri.EscapeDataString(query)}&api_key={_config.ApiKey}";

            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                // Desserialização: PropertyNameCaseInsensitive para mapear snake_case do JSON
                return JsonSerializer.Deserialize<TmdbSearchResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (Exception) // Captura erros de requisição ou JSON
            {
                return null;
            }
        }

        // Método GetMovieDetailAsync declarado APENAS UMA VEZ
        public async Task<MovieDetail?> GetMovieDetailAsync(int id)
        {
            if (string.IsNullOrEmpty(_config.ApiKey))
            {
                return null;
            }

            var url = $"movie/{id}?api_key={_config.ApiKey}";

            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<MovieDetail>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
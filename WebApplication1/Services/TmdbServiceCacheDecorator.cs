// Services/TmdbServiceCacheDecorator.cs

using CatalogoFilmesTempo.Interfaces;
using CatalogoFilmesTempo.Models.Api;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;
using System;

namespace CatalogoFilmesTempo.Services
{
    // Implementa o padrão Decorator para adicionar cache ao TmdbApiService.
    public class TmdbServiceCacheDecorator : ITmdbApiService
    {
        private readonly TmdbApiService _tmdbApiService;
        private readonly IMemoryCache _cache;

        // Define a validade do cache (RF08: 1 hora)
        private static readonly TimeSpan CacheDuration = TimeSpan.FromHours(1);

        // Injeção de Dependência: A implementação original da API e o serviço de cache
        public TmdbServiceCacheDecorator(
            TmdbApiService tmdbApiService,
            IMemoryCache cache)
        {
            _tmdbApiService = tmdbApiService;
            _cache = cache;
        }

        // --- ITmdbApiService Implementation ---

        // Busca de Filmes (RF08: Cache por query e página)
        public async Task<TmdbSearchResponse?> SearchMoviesAsync(string query, int page)
        {
            // Cria uma chave única para a busca
            var cacheKey = $"TmdbSearch_{query}_{page}";

            // Tenta obter o resultado do cache
            if (_cache.TryGetValue(cacheKey, out TmdbSearchResponse? cachedResult) && cachedResult != null)
            {
                // Se estiver no cache, retorna imediatamente
                return cachedResult;
            }

            // Se não estiver no cache, chama a API original
            var apiResult = await _tmdbApiService.SearchMoviesAsync(query, page);

            // Se a chamada à API for bem-sucedida, armazena no cache
            if (apiResult != null)
            {
                // Configura a expiração do cache (1 hora)
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(CacheDuration);

                _cache.Set(cacheKey, apiResult, cacheEntryOptions);
            }

            return apiResult;
        }

        // Detalhes do Filme (RF08: Cache por ID do filme)
        public async Task<MovieDetail?> GetMovieDetailAsync(int tmdbId)
        {
            var cacheKey = $"TmdbDetail_{tmdbId}";

            // Tenta obter o resultado do cache
            if (_cache.TryGetValue(cacheKey, out MovieDetail? cachedDetail) && cachedDetail != null)
            {
                return cachedDetail;
            }

            // Se não estiver no cache, chama a API original
            var apiDetail = await _tmdbApiService.GetMovieDetailAsync(tmdbId);

            // Se a chamada à API for bem-sucedida, armazena no cache
            if (apiDetail != null)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(CacheDuration);

                _cache.Set(cacheKey, apiDetail, cacheEntryOptions);
            }

            return apiDetail;
        }
    }
}
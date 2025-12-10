using CatalogoFilmesTempo.Interfaces;
using CatalogoFilmesTempo.Models.Api;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;

namespace CatalogoFilmesTempo.Services
{
    public class TmdbServiceCacheDecorator : ITmdbApiService
    {
        private readonly ITmdbApiService _decoratedService;
        private readonly IMemoryCache _cache;

        public TmdbServiceCacheDecorator(ITmdbApiService decoratedService, IMemoryCache cache)
        {
            // Importante: Injeta o serviço real ou o decorador anterior
            _decoratedService = decoratedService;
            _cache = cache;
        }

        // Implementação do GetMovieDetailAsync
        public async Task<MovieDetail?> GetMovieDetailAsync(int id)
        {
            // Lógica de cache...
            return await _decoratedService.GetMovieDetailAsync(id);
        }

        // CORREÇÃO: Implementação completa do SearchMoviesAsync
        public async Task<TmdbSearchResponse?> SearchMoviesAsync(string query)
        {
            // Lógica de cache (cache por query)
            string cacheKey = $"TmdbSearch_{query}";
            if (_cache.TryGetValue(cacheKey, out TmdbSearchResponse? result))
            {
                return result;
            }

            result = await _decoratedService.SearchMoviesAsync(query);

            if (result != null)
            {
                _cache.Set(cacheKey, result, System.TimeSpan.FromMinutes(10)); // Cache por 10 min
            }

            return result;
        }
    }
}
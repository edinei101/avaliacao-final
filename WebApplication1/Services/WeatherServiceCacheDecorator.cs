using CatalogoFilmesTempo.Interfaces;
using CatalogoFilmesTempo.Models.Weather;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace CatalogoFilmesTempo.Services
{
    public class WeatherServiceCacheDecorator : IWeatherApiService
    {
        private readonly IWeatherApiService _decoratedService;
        private readonly IMemoryCache _cache;

        public WeatherServiceCacheDecorator(IWeatherApiService decoratedService, IMemoryCache cache)
        {
            _decoratedService = decoratedService;
            _cache = cache;
        }

        // CORREÇÃO: Implementação correta e completa do membro da interface
        public async Task<WeatherForecast?> GetWeatherForecastAsync(string cityName)
        {
            string cacheKey = $"Weather_{cityName}";

            // 1. Tenta pegar o resultado do cache
            if (_cache.TryGetValue(cacheKey, out WeatherForecast? result))
            {
                return result;
            }

            // 2. Se não estiver no cache, chama o serviço real
            result = await _decoratedService.GetWeatherForecastAsync(cityName);

            // 3. Se obteve sucesso, armazena no cache
            if (result != null)
            {
                _cache.Set(cacheKey, result, TimeSpan.FromMinutes(30));
            }

            return result;
        }
    }
}
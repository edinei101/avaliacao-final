// Controllers/FilmesController.cs
using Microsoft.AspNetCore.Mvc;
using CatalogoFilmesTempo.Interfaces;
using CatalogoFilmesTempo.Models.Api;
using CatalogoFilmesTempo.Models.Weather;
using CatalogoFilmesTempo.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using CatalogoFilmesTempo.Repositories;



// O MovieWeatherViewModel deve estar definido (no Models/ ou como abaixo)
public class MovieWeatherViewModel
{
    public MovieDetail Movie { get; set; } = new MovieDetail();
    public WeatherForecast Weather { get; set; } = new WeatherForecast();
}

namespace CatalogoFilmesTempo.Controllers
{
    public class FilmesController : Controller
    {
        private readonly IFilmeRepository _filmeRepository;
        private readonly ITmdbApiService _tmdbApiService;
        private readonly IWeatherApiService _weatherApiService;

        public FilmesController(IFilmeRepository filmeRepository, ITmdbApiService tmdbApiService, IWeatherApiService weatherApiService)
        {
            _filmeRepository = filmeRepository;
            _tmdbApiService = tmdbApiService;
            _weatherApiService = weatherApiService;
        }

        public IActionResult Index()
        {
            return View("Buscar");
        }

        // Action que processa a busca de filmes
        [HttpGet]
        public async Task<IActionResult> Buscar(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                // Retorna uma lista vazia
                return View("Buscar", new List<TmdbSearchResult>());
            }

            var response = await _tmdbApiService.SearchMoviesAsync(query);

            // *** CORREÇÃO: Usando 'is null' para verificar nulidade e .Results (Maiúsculo) ***
            if (response is null || response.Results is null)
            {
                // Em caso de falha da API, retorna uma lista vazia.
                return View("Buscar", new List<TmdbSearchResult>());
            }

            // Passa a lista de resultados (response.Results) para a view de busca
            return View("Buscar", response.Results);
        }

        // Action que exibe os detalhes de um filme específico E a previsão do tempo local
        public async Task<IActionResult> Details(int id)
        {
            var movieDetail = await _tmdbApiService.GetMovieDetailAsync(id);

            // Coordenadas de exemplo para teste
            double latitude = -10.88;
            double longitude = -61.94;

            var weatherForecast = await _weatherApiService.GetWeatherForecastAsync(latitude, longitude);

            var viewModel = new MovieWeatherViewModel
            {
                // Verifica se movieDetail é nulo antes de atribuir
                Movie = movieDetail ?? new MovieDetail(),
                Weather = weatherForecast ?? new WeatherForecast()
            };

            // Se o filme for nulo, redireciona para um erro ou página 404
            if (movieDetail is null)
            {
                return NotFound();
            }

            return View(viewModel);
        }
    }
}
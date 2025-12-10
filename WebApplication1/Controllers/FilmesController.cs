using CatalogoFilmesTempo.Interfaces;
using CatalogoFilmesTempo.Models;
using CatalogoFilmesTempo.Models.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace CatalogoFilmesTempo.Controllers
{
    public class FilmesController : Controller
    {
        private readonly IFilmeRepository _filmeRepository;
        private readonly ITmdbApiService _tmdbApiService;
        private readonly IWeatherApiService _weatherApiService;
        private readonly TmdbConfiguration _tmdbConfig;

        public FilmesController(
            IFilmeRepository filmeRepository,
            ITmdbApiService tmdbApiService,
            IWeatherApiService weatherApiService,
            IOptions<TmdbConfiguration> tmdbConfigOptions)
        {
            _filmeRepository = filmeRepository;
            _tmdbApiService = tmdbApiService;
            _weatherApiService = weatherApiService;
            _tmdbConfig = tmdbConfigOptions.Value;
        }

        // GET: /Filmes/Buscar
        public IActionResult Buscar()
        {
            return View();
        }

        // POST: /Filmes/Buscar
        [HttpPost]
        public async Task<IActionResult> Buscar(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                ViewBag.SearchError = "Por favor, digite um termo para buscar.";
                return View();
            }

            var response = await _tmdbApiService.SearchMoviesAsync(query);

            if (response == null || response.Results == null || response.Results.Count == 0)
            {
                ViewBag.SearchError = $"Nenhum filme encontrado para '{query}'.";
                return View();
            }

            // CORREÇÃO ESSENCIAL: Retorna para a View "Buscar" (que espera MovieDetail)
            return View("Buscar", response.Results);
        }

        // Os métodos Details, Salvar, e List estão corretos e foram mantidos.

        // GET: /Filmes/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var movieDetail = await _tmdbApiService.GetMovieDetailAsync(id);

            if (movieDetail == null)
            {
                return NotFound();
            }

            var weatherForecast = await _weatherApiService.GetWeatherForecastAsync("Curitiba");

            var localFilme = await _filmeRepository.GetByTmdbIdAsync(id);

            ViewBag.Weather = weatherForecast;
            ViewBag.TmdbImageBaseUrl = _tmdbConfig.TmdbImageBaseUrl;
            ViewBag.IsSaved = localFilme != null;

            return View(movieDetail);
        }

        // GET: /Filmes/Salvar/5
        public async Task<IActionResult> Salvar(int id)
        {
            var movieDetail = await _tmdbApiService.GetMovieDetailAsync(id);

            if (movieDetail == null)
            {
                return NotFound();
            }

            var localFilme = await _filmeRepository.GetByTmdbIdAsync(id);
            if (localFilme != null)
            {
                TempData["Message"] = "Este filme já está salvo no catálogo local.";
                return RedirectToAction(nameof(Details), new { id = id });
            }

            var filmeParaSalvar = new Filme
            {
                TmdbId = movieDetail.Id,
                Titulo = movieDetail.Title,
                Sinopse = movieDetail.Overview ?? "Sinopse não disponível.",
                CaminhoPoster = movieDetail.PosterPath ?? string.Empty,
                DataLancamento = movieDetail.ReleaseDate ?? System.DateTime.MinValue,
                DuracaoMinutos = movieDetail.Runtime ?? 0,

                CidadeReferencia = "Curitiba",
                Latitude = -25.4284,
                Longitude = -49.2733
            };

            await _filmeRepository.AddAsync(filmeParaSalvar);

            TempData["Message"] = $"Filme '{movieDetail.Title}' salvo com sucesso no catálogo local!";
            return RedirectToAction(nameof(Details), new { id = id });
        }

        // GET: /Filmes/List (Lista todos os filmes salvos localmente)
        public async Task<IActionResult> List()
        {
            var filmesLocais = await _filmeRepository.ListAsync();
            return View(filmesLocais);
        }
    }
}
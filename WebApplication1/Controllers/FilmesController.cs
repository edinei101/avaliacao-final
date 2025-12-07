// Controllers/FilmesController.cs

using Microsoft.AspNetCore.Mvc;
using CatalogoFilmesTempo.Interfaces;
using CatalogoFilmesTempo.Models;
using CatalogoFilmesTempo.Models.Api;
using CatalogoFilmesTempo.Utils;
using System.Threading.Tasks;

namespace CatalogoFilmesTempo.Controllers
{
    public class FilmesController : Controller
    {
        private readonly IFilmeRepository _filmeRepository;
        private readonly ITmdbApiService _tmdbApiService;
        private readonly IWeatherApiService _weatherApiService;
        private readonly ExportService _exportService;

        public FilmesController(
            IFilmeRepository filmeRepository,
            ITmdbApiService tmdbApiService,
            IWeatherApiService weatherApiService,
            ExportService exportService)
        {
            _filmeRepository = filmeRepository;
            _tmdbApiService = tmdbApiService;
            _weatherApiService = weatherApiService;
            _exportService = exportService;
        }

        // GET: /Filmes/Catalogo
        public async Task<IActionResult> Catalogo()
        {
            var filmes = await _filmeRepository.ListAsync();
            return View("List", filmes);
        }

        // GET: /Filmes/Details/{id} (RF04, RF06)
        public async Task<IActionResult> Details(int id)
        {
            var filme = await _filmeRepository.GetByIdAsync(id);
            if (filme == null)
            {
                return NotFound();
            }

            WeatherForecast? weather = null;
            if (filme.Latitude.HasValue && filme.Longitude.HasValue)
            {
                try
                {
                    weather = await _weatherApiService.GetWeatherForecastAsync(
                        filme.Latitude.Value,
                        filme.Longitude.Value
                    );
                }
                catch (System.Exception ex)
                {
                    ViewData["WeatherError"] = $"Erro ao carregar clima: {ex.Message}";
                }
            }

            ViewData["Weather"] = weather;

            return View(filme);
        }

        // GET: /Filmes/Buscar
        public IActionResult Buscar()
        {
            return View();
        }

        // POST: /Filmes/Buscar (RF02, RF13)
        [HttpPost]
        public async Task<IActionResult> Buscar(string query, int page = 1)
        {
            if (string.IsNullOrEmpty(query))
            {
                ModelState.AddModelError("query", "O termo de busca não pode ser vazio.");
                return View();
            }

            var result = await _tmdbApiService.SearchMoviesAsync(query, page);

            ViewData["CurrentQuery"] = query;

            return View("Buscar", result);
        }

        // GET: /Filmes/Importar/{tmdbId}
        public async Task<IActionResult> Importar(int tmdbId)
        {
            var movieDetail = await _tmdbApiService.GetMovieDetailAsync(tmdbId);
            if (movieDetail == null)
            {
                TempData["ErrorMessage"] = "Detalhes do filme não encontrados no TMDb.";
                return RedirectToAction(nameof(Buscar));
            }

            var filme = new Filme
            {
                TmdbId = movieDetail.id,
                Titulo = movieDetail.title,
                Sinopse = movieDetail.overview,
                DataLancamento = movieDetail.release_date,
                DuracaoMinutos = movieDetail.runtime,
                CaminhoPoster = movieDetail.poster_path,
            };

            return View(filme);
        }

        // POST: /Filmes/Importar (RF03)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Importar([Bind("TmdbId, Titulo, Sinopse, DataLancamento, DuracaoMinutos, CaminhoPoster, CidadeReferencia, Latitude, Longitude")] Filme filme)
        {
            if (string.IsNullOrEmpty(filme.CidadeReferencia) || !filme.Latitude.HasValue || !filme.Longitude.HasValue)
            {
                ModelState.AddModelError("CidadeReferencia", "Cidade, Latitude e Longitude são obrigatórios para a importação.");
            }

            ModelState.Remove("Id");

            if (ModelState.IsValid)
            {
                var filmeExistente = await _filmeRepository.GetByTmdbIdAsync(filme.TmdbId);
                if (filmeExistente != null)
                {
                    TempData["ErrorMessage"] = $"O filme '{filme.Titulo}' já está no seu catálogo local.";
                    return View(filme);
                }

                await _filmeRepository.AddAsync(filme);
                TempData["SuccessMessage"] = $"Filme '{filme.Titulo}' importado com sucesso para o catálogo local!";

                return RedirectToAction(nameof(Catalogo));
            }

            return View(filme);
        }

        // GET: /Filmes/ExportarCsv (RF12)
        public async Task<IActionResult> ExportarCsv()
        {
            var filmes = await _filmeRepository.ListAsync();

            var csvData = _exportService.ExportarFilmesParaCsv(filmes);

            return File(
                fileContents: System.Text.Encoding.UTF8.GetBytes(csvData),
                contentType: "text/csv",
                fileDownloadName: "catalogo_filmes_tempo.csv"
            );
        }

        // GET: /Filmes/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            var filme = await _filmeRepository.GetByIdAsync(id);
            if (filme == null)
            {
                return NotFound();
            }
            return View(filme);
        }

        // POST: /Filmes/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, TmdbId, Titulo, Sinopse, DataLancamento, DuracaoMinutos, CaminhoPoster, CidadeReferencia, Latitude, Longitude")] Filme filme)
        {
            if (id != filme.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _filmeRepository.UpdateAsync(filme);
                TempData["SuccessMessage"] = $"Filme '{filme.Titulo}' atualizado com sucesso!";
                return RedirectToAction(nameof(Catalogo));
            }
            return View(filme);
        }

        // GET: /Filmes/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            var filme = await _filmeRepository.GetByIdAsync(id);
            if (filme == null)
            {
                return NotFound();
            }
            return View(filme);
        }

        // POST: /Filmes/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var filme = await _filmeRepository.GetByIdAsync(id);
            if (filme != null)
            {
                await _filmeRepository.DeleteAsync(id); // Chamada corrigida
                TempData["SuccessMessage"] = $"Filme '{filme.Titulo}' excluído com sucesso do catálogo.";
            }
            return RedirectToAction(nameof(Catalogo));
        }
    }
}
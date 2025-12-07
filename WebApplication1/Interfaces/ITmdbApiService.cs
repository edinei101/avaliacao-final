// Interfaces/ITmdbApiService.cs

using CatalogoFilmesTempo.Models.Api;
using System.Threading.Tasks;

namespace CatalogoFilmesTempo.Interfaces
{
    // Define os métodos para interagir com a API TMDb.
    public interface ITmdbApiService
    {
        // Busca filmes por query, com suporte à paginação.
        Task<TmdbSearchResponse?> SearchMoviesAsync(string query, int page);

        // Obtém detalhes completos de um filme pelo ID.
        Task<MovieDetail?> GetMovieDetailAsync(int tmdbId);
    }
}
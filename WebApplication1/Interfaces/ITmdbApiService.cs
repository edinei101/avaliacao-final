// Interfaces/ITmdbApiService.cs
using CatalogoFilmesTempo.Models.Api;
using System.Threading.Tasks;

namespace CatalogoFilmesTempo.Interfaces
{
    public interface ITmdbApiService
    {
        // Torna o tipo de retorno anulável (TmdbSearchResponse?)
        Task<TmdbSearchResponse?> SearchMoviesAsync(string query);
        Task<MovieDetail?> GetMovieDetailAsync(int id);
    }
}
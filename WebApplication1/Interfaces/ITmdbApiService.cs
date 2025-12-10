using CatalogoFilmesTempo.Models.Api;
using System.Threading.Tasks;

namespace CatalogoFilmesTempo.Interfaces
{
    public interface ITmdbApiService
    {
        // RF05: Busca de filmes (usada no /Filmes/Buscar)
        Task<TmdbSearchResponse?> SearchMoviesAsync(string query);

        // RF05: Detalhes do filme (usada no /Filmes/Details)
        Task<MovieDetail?> GetMovieDetailAsync(int id);
    }
}
// Interfaces/IFilmeRepository.cs

using CatalogoFilmesTempo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatalogoFilmesTempo.Interfaces
{
    // Define os métodos que a classe FilmeRepository DEVE implementar.
    public interface IFilmeRepository
    {
        // CRUD: Read
        Task<IEnumerable<Filme>> ListAsync();
        Task<Filme?> GetByIdAsync(int id);
        Task<Filme?> GetByTmdbIdAsync(int tmdbId); // Para verificar duplicidade na importação

        // CRUD: Create, Update, Delete
        Task AddAsync(Filme filme);
        Task UpdateAsync(Filme filme);
        Task DeleteAsync(int id);
    }
}
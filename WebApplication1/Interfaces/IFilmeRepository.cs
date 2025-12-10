using CatalogoFilmesTempo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatalogoFilmesTempo.Interfaces
{
    public interface IFilmeRepository
    {
        // CRUD Básico
        Task<IEnumerable<Filme>> GetAllAsync();
        Task<Filme?> GetByIdAsync(int id);
        Task AddAsync(Filme filme);
        Task UpdateAsync(Filme filme);
        Task DeleteAsync(int id);

        // Funções Específicas
        Task<IEnumerable<Filme>> ListAsync();
        Task<Filme?> GetByTmdbIdAsync(int tmdbId);
        Task<IEnumerable<Filme>> SearchAsync(string query);
    }
}
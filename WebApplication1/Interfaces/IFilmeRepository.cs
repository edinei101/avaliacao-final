// Repositories/IFilmeRepository.cs (Antiga Interfaces/IFilmeRepository)
using CatalogoFilmesTempo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatalogoFilmesTempo.Repositories
{
    public interface IFilmeRepository
    {
        Task<IEnumerable<Filme>> GetAllAsync();
        Task AddAsync(Filme filme);
        Task DeleteAsync(int id);
    }
}
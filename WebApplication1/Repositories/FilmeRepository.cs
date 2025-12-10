using CatalogoFilmesTempo.Data;
using CatalogoFilmesTempo.Interfaces;
using CatalogoFilmesTempo.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoFilmesTempo.Repositories
{
    public class FilmeRepository : IFilmeRepository
    {
        private readonly AppDbContext _context;

        public FilmeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Filme>> GetAllAsync()
        {
            return await _context.Filmes.ToListAsync();
        }

        public async Task<Filme?> GetByIdAsync(int id)
        {
            return await _context.Filmes.FindAsync(id);
        }

        public async Task AddAsync(Filme filme)
        {
            await _context.Filmes.AddAsync(filme);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Filme filme)
        {
            _context.Filmes.Update(filme);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var filme = await GetByIdAsync(id);
            if (filme != null)
            {
                _context.Filmes.Remove(filme);
                await _context.SaveChangesAsync();
            }
        }

        // Implementação de ListAsync
        public async Task<IEnumerable<Filme>> ListAsync()
        {
            return await _context.Filmes.ToListAsync();
        }

        // Implementação de GetByTmdbIdAsync
        public async Task<Filme?> GetByTmdbIdAsync(int tmdbId)
        {
            return await _context.Filmes.FirstOrDefaultAsync(f => f.TmdbId == tmdbId);
        }

        public async Task<IEnumerable<Filme>> SearchAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return Enumerable.Empty<Filme>();
            }
            return await _context.Filmes
                .Where(f => f.Titulo.Contains(query) || f.Sinopse.Contains(query))
                .ToListAsync();
        }
    }
}
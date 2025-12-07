// Repositories/FilmeRepository.cs

using CatalogoFilmesTempo.Data;
using CatalogoFilmesTempo.Interfaces;
using CatalogoFilmesTempo.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatalogoFilmesTempo.Repositories
{
    // Esta classe implementa a interface IFilmeRepository e lida com a persistência no banco de dados.
    public class FilmeRepository : IFilmeRepository
    {
        private readonly AppDbContext _context;

        // Injeção de Dependência do AppDbContext
        public FilmeRepository(AppDbContext context)
        {
            _context = context;
        }

        // --- CRUD: CREATE ---

        public async Task AddAsync(Filme filme)
        {
            _context.Filmes.Add(filme);
            await _context.SaveChangesAsync();
        }

        // --- CRUD: READ ---

        // Retorna todos os filmes do catálogo.
        public async Task<IEnumerable<Filme>> ListAsync()
        {
            return await _context.Filmes.ToListAsync();
        }

        // Busca um filme pelo Id (Chave Primária).
        public async Task<Filme?> GetByIdAsync(int id)
        {
            return await _context.Filmes.FindAsync(id);
        }

        // Busca um filme pelo TmdbId (Usado para verificar duplicidade).
        public async Task<Filme?> GetByTmdbIdAsync(int tmdbId)
        {
            return await _context.Filmes
                                 .FirstOrDefaultAsync(f => f.TmdbId == tmdbId);
        }

        // --- CRUD: UPDATE ---

        public async Task UpdateAsync(Filme filme)
        {
            _context.Filmes.Update(filme);
            await _context.SaveChangesAsync();
        }

        // --- CRUD: DELETE ---

        // Exclui um filme pelo Id.
        public async Task DeleteAsync(int id)
        {
            var filme = await _context.Filmes.FindAsync(id);
            if (filme != null)
            {
                _context.Filmes.Remove(filme);
                await _context.SaveChangesAsync();
            }
        }
    }
}
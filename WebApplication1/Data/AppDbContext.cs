// Data/AppDbContext.cs

using Microsoft.EntityFrameworkCore;
using CatalogoFilmesTempo.Models;

namespace CatalogoFilmesTempo.Data
{
    // O AppDbContext herda de DbContext e é o coração do Entity Framework Core.
    public class AppDbContext : DbContext
    {
        // Construtor necessário para a injeção de dependência no Program.cs
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // DbSet: Representa a coleção de entidades (tabela) no banco de dados.
        // O nome da tabela no SQLite será 'Filmes'.
        public DbSet<Filme> Filmes { get; set; } = default!;

        // Você pode usar este método para configurar chaves e índices (opcional para o nosso caso simples)
        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     base.OnModelCreating(modelBuilder);
        // }
    }
}
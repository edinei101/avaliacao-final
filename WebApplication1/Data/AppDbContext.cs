using CatalogoFilmesTempo.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogoFilmesTempo.Data
{
    // O AppDbContext herda de DbContext, que é a classe base do EF Core.
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Define a coleção de Filmes (tabela 'Filmes') no banco de dados.
        // O EF Core usará as propriedades de 'Filme.cs' para criar as colunas.
        public DbSet<Filme> Filmes { get; set; }

        // Se precisar de alguma configuração de modelo específica (opcional, mas bom ter):
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Exemplo: Garante que o TmdbId é único (opcional, mas recomendado para IDs externos)
            modelBuilder.Entity<Filme>()
                .HasIndex(f => f.TmdbId)
                .IsUnique();
        }
    }
}
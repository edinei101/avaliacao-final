// Interfaces/IExportService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using CatalogoFilmesTempo.Models;

namespace CatalogoFilmesTempo.Interfaces
{
    public interface IExportService
    {
        // Este método será implementado em ExportService.cs
        Task<byte[]> ExportFilmesToCsvAsync(IEnumerable<Filme> filmes);
    }
}
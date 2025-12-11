// Utils/ExportService.cs
using CatalogoFilmesTempo.Interfaces;
using CatalogoFilmesTempo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

// A CORREÇÃO ESTÁ AQUI: O namespace DEVE ser CatalogoFilmesTempo.Utils
namespace CatalogoFilmesTempo.Utils
{
    public class ExportService : IExportService
    {
        public ExportService() { }

        public Task<byte[]> ExportFilmesToCsvAsync(IEnumerable<Filme> filmes)
        {
            return Task.FromResult(new byte[0]);
        }
    }
}
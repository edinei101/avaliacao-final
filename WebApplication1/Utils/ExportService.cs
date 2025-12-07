// Utils/ExportService.cs (CÓDIGO CORRIGIDO)

using CatalogoFilmesTempo.Models;
using System.Collections.Generic;
using System.Text;

namespace CatalogoFilmesTempo.Utils
{
    public class ExportService
    {
        public string ExportarFilmesParaCsv(IEnumerable<Filme> filmes)
        {
            var sb = new StringBuilder();

            // CABEÇALHO CORRIGIDO para incluir apenas campos existentes em Filme.cs
            sb.AppendLine("Id;TmdbId;Titulo;Sinopse;DataLancamento;DuracaoMinutos;CidadeReferencia;Latitude;Longitude");

            foreach (var filme in filmes)
            {
                sb.AppendLine($"{filme.Id};" +
                              $"{filme.TmdbId};" +
                              $"\"{filme.Titulo}\";" +
                              $"\"{filme.Sinopse?.Replace("\"", "\"\"")}\";" +
                              $"{filme.DataLancamento?.ToString("yyyy-MM-dd")};" +
                              $"{filme.DuracaoMinutos};" +
                              $"{filme.CidadeReferencia};" +
                              $"{filme.Latitude};" +
                              $"{filme.Longitude}");
            }

            return sb.ToString();
        }
    }
}
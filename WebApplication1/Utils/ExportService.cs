using CatalogoFilmesTempo.Models;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CatalogoFilmesTempo.Utils
{
    public class ExportService
    {
        public byte[] ExportarFilmesParaCsv(IEnumerable<Filme> filmes)
        {
            var sb = new StringBuilder();

            // Cabeçalho
            sb.AppendLine("ID Local,ID TMDb,Título,Sinopse,Lançamento,Duração (min),Cidade,Latitude,Longitude");

            foreach (var filme in filmes)
            {
                // CORREÇÃO: Removendo o '?' do acesso a DataLancamento, pois em Filme.cs é DateTime não anulável.
                string dataLancamento = filme.DataLancamento.ToString("yyyy-MM-dd");

                sb.AppendLine($"{filme.Id},{filme.TmdbId},\"{filme.Titulo}\",\"{filme.Sinopse.Replace("\"", "\"\"")}\",{dataLancamento},{filme.DuracaoMinutos},\"{filme.CidadeReferencia}\",{filme.Latitude},{filme.Longitude}");
            }

            // Converte a string para array de bytes
            return Encoding.UTF8.GetBytes(sb.ToString());
        }
    }
}
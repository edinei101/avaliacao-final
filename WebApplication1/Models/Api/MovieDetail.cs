// Models/Api/MovieDetail.cs

using System;
using System.Text.Json.Serialization;

namespace CatalogoFilmesTempo.Models.Api
{
    // Modelo que representa os detalhes completos de um filme no TMDb.
    public class MovieDetail
    {
        public int id { get; set; }
        public string? title { get; set; }
        public string? overview { get; set; }

        // release_date será convertido em DataLancamento no nosso modelo Filme.
        public DateTime? release_date { get; set; }

        public int runtime { get; set; } // Duração em minutos

        // poster_path é o caminho parcial do poster.
        public string? poster_path { get; set; }
    }
}
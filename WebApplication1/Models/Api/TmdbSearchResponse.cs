// Models/Api/TmdbSearchResponse.cs

using System.Collections.Generic;

namespace CatalogoFilmesTempo.Models.Api
{
    // Modelo que representa a resposta completa de uma busca de filmes no TMDb.
    public class TmdbSearchResponse
    {
        public int page { get; set; }

        // results contém uma lista simplificada de filmes (MovieResult)
        public List<MovieDetail>? results { get; set; }

        public int total_pages { get; set; }
        public int total_results { get; set; }
    }
}
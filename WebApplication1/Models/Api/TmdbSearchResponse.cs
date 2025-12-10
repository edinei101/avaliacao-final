// Models/Api/TmdbSearchResponse.cs
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CatalogoFilmesTempo.Models.Api
{
    public class TmdbSearchResponse
    {
        [JsonPropertyName("page")]
        public int Page { get; set; }

        // ESSENCIAL: O resultado da busca é uma lista de MovieDetail
        [JsonPropertyName("results")]
        public List<MovieDetail> Results { get; set; } = new List<MovieDetail>();

        [JsonPropertyName("total_pages")]
        public int TotalPages { get; set; }

        [JsonPropertyName("total_results")]
        public int TotalResults { get; set; }
    }
}
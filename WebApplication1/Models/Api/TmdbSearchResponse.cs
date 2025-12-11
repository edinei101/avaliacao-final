// Models/Api/TmdbSearchResponse.cs

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CatalogoFilmesTempo.Models.Api
{
    public class TmdbSearchResponse
    {
        [JsonPropertyName("results")]
        public List<TmdbSearchResult>? Results { get; set; }

        [JsonPropertyName("page")]
        public int Page { get; set; }

        [JsonPropertyName("total_pages")]
        public int TotalPages { get; set; }

        [JsonPropertyName("total_results")]
        public int TotalResults { get; set; }

        public TmdbSearchResponse()
        {
            Results = new List<TmdbSearchResult>();
        }
    }
}
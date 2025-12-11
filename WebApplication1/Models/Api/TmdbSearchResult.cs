// Models/Api/TmdbSearchResult.cs
using System; // <--- GARANTA QUE ESTE USING ESTÁ AQUI
using System.Text.Json.Serialization;

namespace CatalogoFilmesTempo.Models.Api
{
    public class TmdbSearchResult
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("overview")]
        public string Overview { get; set; } = string.Empty;

        [JsonPropertyName("poster_path")]
        public string PosterPath { get; set; } = string.Empty;

        [JsonPropertyName("release_date")]
        public string ReleaseDate { get; set; } = string.Empty;

        // ESTA É A PROPRIEDADE QUE SUA VIEW PRECISA:
        public int ReleaseYear => string.IsNullOrEmpty(ReleaseDate) ? 0 :
                                 DateTime.TryParse(ReleaseDate, out var date) ? date.Year : 0;
    }
}
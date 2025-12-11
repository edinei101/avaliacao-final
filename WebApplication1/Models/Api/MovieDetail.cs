// Models/Api/MovieDetail.cs

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CatalogoFilmesTempo.Models.Api
{
    public class MovieDetail
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        // --- PROPRIEDADES NECESSÁRIAS PELO CÓDIGO (Views) ---

        [JsonPropertyName("overview")]
        public string Overview { get; set; } = string.Empty; // Necessário pela View Details

        [JsonPropertyName("poster_path")]
        public string PosterPath { get; set; } = string.Empty; // Necessário pelas Views

        [JsonPropertyName("release_date")]
        public string ReleaseDate { get; set; } = string.Empty; // Necessário pelas Views

        [JsonPropertyName("runtime")]
        public int? Runtime { get; set; } // Necessário pela View Details

        [JsonPropertyName("vote_average")]
        public double VoteAverage { get; set; } // Necessário pela View Details

        [JsonPropertyName("vote_count")]
        public int VoteCount { get; set; } // Necessário pela View Details

        // ----------------------------------------------------

        [JsonPropertyName("genres")]
        public List<Genre> Genres { get; set; } = new List<Genre>();

        // Outros campos...
    }

    public class Genre
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }
}
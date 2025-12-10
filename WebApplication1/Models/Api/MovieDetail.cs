using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CatalogoFilmesTempo.Models.Api
{
    // Classe aninhada para os gêneros
    public class Genre
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }

    public class MovieDetail
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("overview")]
        public string? Overview { get; set; }

        [JsonPropertyName("poster_path")]
        public string? PosterPath { get; set; }

        // O formato da data pode ser nulo se o filme não foi lançado
        [JsonPropertyName("release_date")]
        public DateTime? ReleaseDate { get; set; }

        // O runtime pode ser nulo
        [JsonPropertyName("runtime")]
        public int? Runtime { get; set; }

        [JsonPropertyName("vote_average")]
        public double VoteAverage { get; set; }

        [JsonPropertyName("vote_count")]
        public int VoteCount { get; set; }

        [JsonPropertyName("genres")]
        public List<Genre> Genres { get; set; } = new List<Genre>();
    }
}
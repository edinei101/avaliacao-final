// Models/Filme.cs

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogoFilmesTempo.Models
{
    // Modelo que representa um filme no catálogo local (tabela Filmes no SQLite).
    public class Filme
    {
        [Key]
        // Id: Chave primária auto-incrementável (Primary Key)
        public int Id { get; set; }

        [Required]
        // TmdbId: ID do filme na API externa (usado para garantir unicidade e referência)
        public int TmdbId { get; set; }

        [Required(ErrorMessage = "O Título é obrigatório.")]
        [StringLength(200)]
        public string? Titulo { get; set; }

        [StringLength(2000)]
        public string? Sinopse { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de Lançamento")]
        // DataLancamento: Usamos DateTime? (anulável) porque nem todos os filmes podem ter uma data definida.
        public DateTime? DataLancamento { get; set; }

        [Display(Name = "Duração (min)")]
        public int DuracaoMinutos { get; set; }

        // CaminhoPoster: A parte final da URL do poster fornecida pelo TMDb.
        public string? CaminhoPoster { get; set; }

        // --- Campos de Referência Geográfica (RF03/RF06) ---

        [Required(ErrorMessage = "A Cidade de Referência é obrigatória para obter o clima.")]
        [StringLength(100)]
        [Display(Name = "Cidade de Referência")]
        // CidadeReferencia: O nome da cidade que o usuário associa ao filme.
        public string? CidadeReferencia { get; set; }

        // Latitude e Longitude: Armazenam as coordenadas para buscar a previsão do tempo.
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
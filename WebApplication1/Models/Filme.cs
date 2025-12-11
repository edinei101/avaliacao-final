// Models/Filme.cs

using System;
using System.ComponentModel.DataAnnotations;

namespace CatalogoFilmesTempo.Models
{
    public class Filme
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O título é obrigatório.")]
        public string Titulo { get; set; } = string.Empty;

        public int TmdbId { get; set; }

        // --- PROPRIEDADES NECESSÁRIAS PELO CÓDIGO (Views/Repositories) ---
        public string Sinopse { get; set; } = string.Empty;

        [Display(Name = "Caminho Poster")]
        public string CaminhoPoster { get; set; } = string.Empty;

        [Display(Name = "Data Lançamento")]
        public DateTime? DataLancamento { get; set; } // Necessário pelo _MovieCardPartial.cshtml

        // Campo ImagemUrl usado em algumas versões
        public string ImagemUrl { get; set; } = string.Empty;
        // -----------------------------------------------------------------
    }
}
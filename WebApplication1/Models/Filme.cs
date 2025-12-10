using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogoFilmesTempo.Models
{
    public class Filme
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // ID externo do TMDb para referência (Chave única no AppDbContext)
        [Required]
        public int TmdbId { get; set; }

        [Required]
        [StringLength(200)]
        public string Titulo { get; set; } = string.Empty;

        // Usado no Repositório e Controller
        public string Sinopse { get; set; } = string.Empty;

        // Usado no Repositório e Controller
        public string CaminhoPoster { get; set; } = string.Empty;

        public int DuracaoMinutos { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataLancamento { get; set; }

        // RF02: Dados de localização padrão (para o clima)
        public string CidadeReferencia { get; set; } = "Curitiba";
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
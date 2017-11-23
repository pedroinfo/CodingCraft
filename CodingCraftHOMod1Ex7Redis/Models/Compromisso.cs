using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingCraftHOMod1Ex7Redis.Models
{
    [Table("Compromisso")]
    public class Compromisso
    {
        [Key]
        public int CompromissoId { get; set; }

        [Required]
        [Display(Name ="Título")]
        public string Titulo { get; set; }

        [Required]
        [Display(Name = "Data/Horário")]
        public DateTime DataHora { get; set; }
        
        [Required]
        public string Local { get; set; }

        [Required]
        [Display(Name = "Observações")]
        public string Observacoes { get; set; }
    }
}
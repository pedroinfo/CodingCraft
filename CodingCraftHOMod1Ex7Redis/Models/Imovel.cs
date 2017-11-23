using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingCraftHOMod1Ex7Redis.Models
{
    [Table("Imovel")]
    public class Imovel
    {
        [Key]
        public int ImovelId { get; set; }

        [Display(Name = "Título")]
        public string Titulo { get; set; }

        [Display(Name = "Valor")]
        public decimal Valor { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }


    }
}
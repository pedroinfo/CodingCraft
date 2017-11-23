using CodingCraftHOMod1Ex7Redis.Util;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingCraftHOMod1Ex7Redis.Models
{
    [Table("Empresa")]
    public class Empresa
    {
        [Key]
        public int EmpresaId { get; set; }

        [Required]
        [Display(Name = "Razão Social")]
        public string RazaoSocial { get; set; }

        [Required]
        [Display(Name = "Nome Fantasia")]
        public string NomeFantasia { get; set; }

        [Required]
        [Display(Name = "CNPJ")]
        public string Cnpj { get; set; }
    }
}
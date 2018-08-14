using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CodingCraftHOMod1Ex9I18n.Models
{
    [Table("Livro")]
    public class Livro
    {
        [Key]
        public int LivroId { get; set; }

        [Required]
        public string Titulo { get; set; }

        [Required]
        public string Editora { get; set; }

        [Required]
        public string Assunto { get; set; }

        [Required]
        public string Descricao { get; set; }

        
        public decimal Preco { get; set; }

        public byte[] Imagem { get; set; } 

    }
}
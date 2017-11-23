using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CodingCraftHOMod1Ex7Redis.Models
{
    [Table("Contato")]
    public class Contato
    {
        [Key]
        public int ContatoId { get; set; }

        [Required(ErrorMessage = "Preencha o campo Nome")]
        public string Nome { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Preencha o campo e-mail corretamente")]
        public string Email { get; set; }

        public string Telefone { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }
    }
}
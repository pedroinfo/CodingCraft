using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CodingCraftHOMod1Ex1EF.Models
{
    public class Clientes
    {
        [Key]
        [Display(Name = "Cliente")]
        public int ClienteId { get; set; }

        [Required]
        [StringLength(100)]
        [Index(IsUnique = true)]
        public string Nome { get; set; }


        [Required]
        [Display(Name = "Endereço de e-mail")]
        [EmailAddress(ErrorMessage = "Endereço de e-mail inválido")]
        [StringLength(100)]
        [Index(IsUnique = true)]
        public string Email { get; set; }
    }
}
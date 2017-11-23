using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingCraftHOMod1Ex1EF.Models
{
    public class Loja : Entidade
    {
        [Key]
        public int LojaId { get; set; }

        [Required]
        [StringLength(100)]
        [Index(IsUnique = true)]
        [Display(Name ="Loja")]
        public String Nome { get; set; }

        public virtual ICollection<Estoque> LojaEstoques { get; set; }
    }
}
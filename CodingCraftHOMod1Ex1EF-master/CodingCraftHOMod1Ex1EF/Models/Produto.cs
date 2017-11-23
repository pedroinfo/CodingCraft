using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingCraftHOMod1Ex1EF.Models
{
    [Table("Produtos")]
    public class Produto : Entidade
    {
        [Key]
        
        public int ProdutoId { get; set; }
        public int CategoriaId { get; set; }

        [Required]
        [StringLength(200)]
        [Index(IsUnique = true)]
        [Display(Name = "Produto")]
        public String Nome { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Preço")]
        public decimal Preco { get; set; }

        public virtual Categoria Categoria { get; set; }

        public virtual ICollection<Estoque> ProdutoEstoques { get; set; }
    }
}
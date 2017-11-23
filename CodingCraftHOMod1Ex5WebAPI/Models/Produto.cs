using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingCraftHOMod1Ex5WebAPI.Models
{
    public class Produto
    {
        [Key]
        public Guid ProdutoId { get; set; }
        public Guid? CategoriaId { get; set; }

        [Required]
        [StringLength(200)]
        [Index(IsUnique = true)]
        public string Descricao { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Preco { get; set; }

        public virtual Categoria Categoria { get; set; }
    }
}
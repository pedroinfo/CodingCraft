using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingCraftHOMod1Ex1EF.Models
{
    public class Estoque : Entidade
    {
        [Key]
        public int EstoqueId { get; set; }
        [Index("IUQ_Estoques_ProdutoId_LojaId", IsUnique = true, Order = 1)]
        // [Key, Column(Order = 1)]
        public int ProdutoId { get; set; }
        [Index("IUQ_Estoques_ProdutoId_LojaId", IsUnique = true, Order = 2)]
        // [Key, Column(Order = 2)]
        public int LojaId { get; set; }

        [Required]
        public int Quantidade { get; set; }

        public virtual Produto Produto { get; set; }
        public virtual Loja Loja { get; set; }
    }
}
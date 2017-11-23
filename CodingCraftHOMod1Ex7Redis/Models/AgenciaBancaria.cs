using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingCraftHOMod1Ex7Redis.Models
{
    [Table("AgenciasBancarias")]
    public class AgenciaBancaria
    {
        [Key]
        public int AgenciaBancariaId { get; set; }
        // [Index("IUQ_AgenciasBancarias_BancoId_CodigoCompensacao", IsUnique = true, Order = 1)]
        public int BancoId { get; set; }

        [Required]
        // [Index("IUQ_AgenciasBancarias_BancoId_CodigoCompensacao", IsUnique = true, Order = 2)]
        public int CodigoCompensacao { get; set; }

        [StringLength(200)]
        public String Nome { get; set; }

        public virtual Banco Banco { get; set; }
    }
}
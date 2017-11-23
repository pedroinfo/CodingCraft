using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingCraftHOMod1Ex7Redis.Models
{
    [Table("Banco")]
    public class Banco
    {
        [Key]
        public int BancoId { get; set; }

        
        [StringLength(200)]
        [Index("IUQ_Bancos_Nome")]
        public String Nome { get; set; }

        [Display(Name = "Número")]
        public int Numero { get; set; }

        [JsonIgnore]
        public virtual ICollection<AgenciaBancaria> AgenciasBancarias { get; set; }
    }
}
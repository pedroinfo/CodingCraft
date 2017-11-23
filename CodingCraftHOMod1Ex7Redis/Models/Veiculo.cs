using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CodingCraftHOMod1Ex7Redis.Models
{
    [Table("Veiculo")]
    public class Veiculo
    {
        [Key]
        public int VeiculoId { get; set; }

        [Required]
        public string Carro { get; set; }

        [Required]
        public string Marca { get; set; }

        [Required]
        public short Ano { get; set; }

        [Required]
        public string Placa { get; set; }

    }
}
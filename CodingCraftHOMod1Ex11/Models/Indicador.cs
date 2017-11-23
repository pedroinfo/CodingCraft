using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EX11.Models
{
    [Table("Indicador")]
    public class Indicador
    {
        [Key]
        public int IndicadorId { get; set; }
        public string Nome { get; set; }
    }
}
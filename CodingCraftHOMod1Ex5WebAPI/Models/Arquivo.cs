using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CodingCraftHOMod1Ex5WebAPI.Models
{
    [Table("Arquivo")]
    [KnownTypeAttribute(typeof(Arquivo))]
    public class Arquivo
    {
        [Key]
        public int ArquivoId { get; set; }
        public int ArquivoTipoId { get; set; }

        public string NomeArquivo { get; set; }
        public string Path { get; set; }
        public DateTime DataEntrada { get; set; }

        public virtual ArquivoTipo ArquivoTipo { get; set; }
    }
}
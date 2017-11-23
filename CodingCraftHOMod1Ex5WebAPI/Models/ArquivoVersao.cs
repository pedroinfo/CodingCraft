using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CodingCraftHOMod1Ex5WebAPI.Models
{
    [Table("ArquivoVersao")]
    [KnownTypeAttribute(typeof(ArquivoVersao))]
    public class ArquivoVersao
    {
        [Key]
        public int ArquivoVersaoId { get; set; }
        
        public int ArquivoId { get; set; }
        public int ArquivoTipoId { get; set; }

        public string NomeArquivo { get; set; }
        public string Path { get; set; }
        public DateTime DataEntrada { get; set; }

        public virtual ArquivoTipo ArquivoTipo { get; set; }
       // public virtual Arquivo Arquivo { get; set; }
    }
}
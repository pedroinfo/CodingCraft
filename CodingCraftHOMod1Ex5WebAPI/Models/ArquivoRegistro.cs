using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CodingCraftHOMod1Ex5WebAPI.Models
{
    [Table("ArquivoRegistro")]
    [KnownTypeAttribute(typeof(ArquivoRegistro))]
    public class ArquivoRegistro
    {
        [Key]
        public int ArquivoRegistroId { get; set; }

        public int ArquivoId { get; set; }

        public DateTime DataDownload { get; set; }

        public virtual Arquivo Arquivo { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CodingCraftHOMod1Ex5WebAPI.Models
{
    [Table("ArquivoTipo")]
    [KnownTypeAttribute(typeof(ArquivoTipo))]
    public class ArquivoTipo
    {
        [Key]
        public int ArquivoTipoId { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [StringLength(100)]
        public string Descricao { get; set; }
    }
}
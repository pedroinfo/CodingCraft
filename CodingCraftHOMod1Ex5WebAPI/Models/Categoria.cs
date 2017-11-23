using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingCraftHOMod1Ex5WebAPI.Models
{
    public class Categoria
    {
        [Key]
        public Guid CategoriaId { get; set; }

        [Required]
        [StringLength(100)]
        [Index(IsUnique = true)]
        public String Nome { get; set; }

        [JsonIgnore]
        public virtual ICollection<Produto> Produto { get; set; }
    }
}
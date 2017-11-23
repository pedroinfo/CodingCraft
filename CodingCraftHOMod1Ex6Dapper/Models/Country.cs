using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingCraftHOMod1Ex6Dapper.Models
{
    public class Country
    {
        [Key]
        public int CountryId { get; set; }

        [Required]
        [StringLength(200)]
        [Index("IUQ_Countries_Name")]
        public String Name { get; set; }

        public virtual ICollection<AgricultureGdp> AgricultureGdps { get; set; }
    }
}
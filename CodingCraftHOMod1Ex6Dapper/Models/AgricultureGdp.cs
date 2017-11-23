using RichardLawley.EF.AttributeConfig;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingCraftHOMod1Ex6Dapper.Models
{
    public class AgricultureGdp
    {
        [Key]
        public int AgricultureGdpId { get; set; }
        [Index("IUQ_AgricultureGdps_CountryId_Year", IsUnique = true, Order = 1)]
        public int CountryId { get; set; }

        [Display(Name = "Ano")]
        [Index("IUQ_AgricultureGdps_CountryId_Year", IsUnique = true, Order = 2)]
        public short Year { get; set; }

        [Display(Name = "Valor")]
        [DecimalPrecision(20, 15)]
        public decimal? Value { get; set; } = 0;

        public virtual Country Country { get; set; }
    }
}
using Microsoft.AspNet.Identity.EntityFramework;
using RichardLawley.EF.AttributeConfig;
using System.Data.Entity;

namespace CodingCraftHOMod1Ex6Dapper.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Add conventions for Precision Attributes
            modelBuilder.Conventions.Add(new DecimalPrecisionAttributeConvention());
            modelBuilder.Conventions.Add(new DateTimePrecisionAttributeConvention());
        }

        public DbSet<Country> Countries { get; set; }

        public System.Data.Entity.DbSet<CodingCraftHOMod1Ex6Dapper.Models.AgricultureGdp> AgricultureGdps { get; set; }
    }
}
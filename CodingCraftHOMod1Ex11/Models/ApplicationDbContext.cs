using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EX11.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Pesquisa> Pesquisas { get; set; }
        public DbSet<Pais> Pais { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Indicador> Indicador { get; set; }
    }
}
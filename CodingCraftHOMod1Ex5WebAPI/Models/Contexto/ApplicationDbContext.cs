using CodingCraftHOMod1Ex5WebAPI.Models.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace CodingCraftHOMod1Ex5WebAPI.Models.Contexto
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Configuration.ProxyCreationEnabled = false;
        }
        

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        

        public DbSet<Produto> Produtos { get; set; }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Arquivo> Arquivos { get; set; }

        public DbSet<ArquivoTipo> ArquivosTipos { get; set; }
        
        public DbSet<ArquivoRegistro> ArquivosRegistros { get; set; }

        public DbSet<ArquivoVersao> ArquivosVersoes { get; set; }
    }
}
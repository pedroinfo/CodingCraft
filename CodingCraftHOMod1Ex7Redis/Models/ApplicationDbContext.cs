using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace CodingCraftHOMod1Ex7Redis.Models
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

        public DbSet<Banco> Bancos { get; set; }

        public DbSet<AgenciaBancaria> AgenciasBancarias { get; set; }
        public DbSet<Compromisso> Compromissos { get; set; }
        public DbSet<Contato> Contatos { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        
        public DbSet<Imovel> Imoveis { get; set; }
    }
}
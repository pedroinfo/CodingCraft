using System.Data.Entity;

namespace EX2.Models
{
    public class EX2Context : DbContext
    {
        public EX2Context(): base("DefaultConnection")
        {
        }

        public static EX2Context Create()
        {
            return new EX2Context();
        }
    
        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Produto> Produtoes { get; set; }

        public DbSet<Funcionario> Funcionarios { get; set; }

    }
}
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CodingCraftHOMod1Ex4Identity.Models
{
    public class ApplicationDbContext : IdentityDbContext<Usuario, Grupo, int, UsuarioLogin, UsuarioGrupo, UsuarioIdentificacao>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Usuario>()
                .ToTable("Usuarios", "dbo").Property(p => p.Id).HasColumnName("Id");

            modelBuilder.Entity<UsuarioGrupo>()
                .HasKey(r => new { r.UserId, r.RoleId })
                .ToTable("UsuariosGrupos");

            modelBuilder.Entity<UsuarioLogin>()
                .HasKey(l => new { l.LoginProvider, l.ProviderKey, l.UserId })
                .ToTable("UsuariosLogins");

            modelBuilder.Entity<Grupo>()
                .HasKey(g => new { g.Id })
                .ToTable("Grupos");

            modelBuilder.Entity<UsuarioIdentificacao>()
                .HasKey(ui => new { ui.Id })
                .ToTable("UsuariosIdentificacoes");
        }
    }
}
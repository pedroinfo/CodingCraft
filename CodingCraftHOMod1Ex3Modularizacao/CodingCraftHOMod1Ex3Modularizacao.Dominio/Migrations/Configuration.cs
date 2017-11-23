namespace CodingCraftHOMod1Ex3Modularizacao.Dominio.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CodingCraftHOMod1Ex3Modularizacao.Dominio.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(CodingCraftHOMod1Ex3Modularizacao.Dominio.Models.ApplicationDbContext context)
        {

            context.Cursos.AddOrUpdate(
                c => c.Nome,
                new Curso { Nome = "Artes" },
                new Curso { Nome = "Geografia" },
                new Curso { Nome = "História" },
                new Curso { Nome = "Computação" }
                );
            
            var roles = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            roles.Create(new IdentityRole("Produção"));
            roles.Create(new IdentityRole("Revisão"));
            roles.Create(new IdentityRole("Aprovação"));
            roles.Create(new IdentityRole("Publicação"));
            roles.Create(new IdentityRole("Aluno"));
            
            if (!context.Users.Any(u => u.UserName == "admin@admin.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "admin@admin.com", Email = "admin@admin.com" };
                
                manager.Create(user, "123456");

                manager.AddToRole(user.Id, "Produção");
                manager.AddToRole(user.Id, "Revisão");
                manager.AddToRole(user.Id, "Aprovação");
                manager.AddToRole(user.Id, "Publicação");
                manager.AddToRole(user.Id, "Aluno");
            }
        }
    }
}

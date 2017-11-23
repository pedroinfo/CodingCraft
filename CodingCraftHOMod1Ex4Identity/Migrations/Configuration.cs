namespace CodingCraftHOMod1Ex4Identity.Migrations
{
    using CodingCraftHOMod1Ex4Identity.Infrastructure.Identity;
    using CodingCraftHOMod1Ex4Identity.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CodingCraftHOMod1Ex4Identity.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(CodingCraftHOMod1Ex4Identity.Models.ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<Usuario, Grupo, int, UsuarioLogin, UsuarioGrupo, UsuarioIdentificacao>(context));
            var roleManager = new ApplicationRoleManager(new RoleStore<Grupo, int, UsuarioGrupo>(context));
            const string name = "admin@example.com";
            const string password = "Admin@123456";
            const string roleName = "Admin";

            //Create Role Admin if it does not exist
            var role = roleManager.FindByName(roleName);
            if (role == null)
            {
                role = new Grupo { Name = roleName };
                var roleresult = roleManager.Create(role);
            }

            var user = userManager.FindByName(name);
            if (user == null)
            {
                user = new Usuario { UserName = name, Email = name };
                var result = userManager.Create(user, password);
                result = userManager.SetLockoutEnabled(user.Id, false);
            }

            // Add user admin to Role Admin if not already added
            var rolesForUser = userManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(role.Name))
            {
                var result = userManager.AddToRole(user.Id, role.Name);
            }
        }
    }
}

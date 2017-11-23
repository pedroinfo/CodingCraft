using CodingCraftHOMod1Ex4Identity.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace CodingCraftHOMod1Ex4Identity.Infrastructure.Identity
{
    // Configure the RoleManager used in the application. RoleManager is defined in the ASP.NET Identity core assembly
    public class ApplicationRoleManager : RoleManager<Grupo, int>
    {
        public ApplicationRoleManager(IRoleStore<Grupo, int> roleStore): base(roleStore)
        {
        }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context = null)
        {
            var dbContext = context.Get<ApplicationDbContext>() ?? new ApplicationDbContext();
            return new ApplicationRoleManager(new RoleStore<Grupo, int, UsuarioGrupo>(dbContext));
        }
    }
}
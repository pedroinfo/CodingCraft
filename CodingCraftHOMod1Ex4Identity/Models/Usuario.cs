using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;

namespace CodingCraftHOMod1Ex4Identity.Models
{
    // You can add profile data for the user by adding more properties to your Usuario class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class Usuario : IdentityUser<int, UsuarioLogin, UsuarioGrupo, UsuarioIdentificacao>
    {
        [Display(Name = "Endereço de E-mail")]
        public override string Email { get; set; }

        [Display(Name = "Usuário")]
        public override string UserName { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DataNascimento { get; set; }

        public string Desenvolvedor { get; set; }

        public string UsuarioPremium { get; set; }
        
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Usuario, int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
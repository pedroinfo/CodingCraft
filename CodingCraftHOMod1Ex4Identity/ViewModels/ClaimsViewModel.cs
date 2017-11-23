using System.ComponentModel.DataAnnotations;

namespace CodingCraftHOMod1Ex4Identity.ViewModels
{
    public class ClaimsViewModel
    {
        [Display(Name = "Desenvolvedor")]
        public string Desenvolvedor { get; set; }

        [Display(Name = "Usuário Premium")]
        public string UsuarioPremium { get; set; }
    }
}
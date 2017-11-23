using System.ComponentModel.DataAnnotations;

namespace CodingCraftHOMod1Ex3Modularizacao.ViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}

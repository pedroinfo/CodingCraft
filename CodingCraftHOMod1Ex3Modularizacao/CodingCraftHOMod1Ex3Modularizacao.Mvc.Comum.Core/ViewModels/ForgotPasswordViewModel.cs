using System.ComponentModel.DataAnnotations;

namespace CodingCraftHOMod1Ex3Modularizacao.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}

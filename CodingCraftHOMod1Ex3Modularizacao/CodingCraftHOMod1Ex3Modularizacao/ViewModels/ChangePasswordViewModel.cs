using System.ComponentModel.DataAnnotations;

namespace CodingCraftHOMod1Ex3Modularizacao.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha atual")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Tamanho minimo de caracteres = 6", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nova senha")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar senha")]
        [Compare("NewPassword", ErrorMessage = "Senhas não correspondentes.")]
        public string ConfirmPassword { get; set; }
    }
}
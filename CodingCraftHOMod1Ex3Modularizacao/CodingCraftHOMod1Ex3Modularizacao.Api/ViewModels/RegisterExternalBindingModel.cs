using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace CodingCraftHOMod1Ex3Modularizacao.Api.ViewModels
{
    public class RegisterExternalBindingModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}

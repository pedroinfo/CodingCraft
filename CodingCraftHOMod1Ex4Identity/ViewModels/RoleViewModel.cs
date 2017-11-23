using System.ComponentModel.DataAnnotations;

namespace CodingCraftHOMod1Ex4Identity.ViewModels
{
    public class RoleViewModel
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Nome do Grupo")]
        public string Name { get; set; }
    }
}
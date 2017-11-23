using System.ComponentModel.DataAnnotations;

namespace IdentityMvc.Models
{
    public class ViewRole
    {
        [Key]
        public string Id { get; set; }

        [Required(ErrorMessage = "Nome do grupo é necessário !")]
        [Display(Name = "Nome do grupo")]
        public string Name { get; set; }
    }
}
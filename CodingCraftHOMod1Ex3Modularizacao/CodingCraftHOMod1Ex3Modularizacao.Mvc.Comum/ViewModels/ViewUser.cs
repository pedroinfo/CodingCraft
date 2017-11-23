using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using CodingCraftHOMod1Ex3Modularizacao.Mvc.Comum.ViewModels;

namespace IdentityMvc.Models
{
    public class ViewUser
    {
        public string Id { get; set; }
        public string Email { get; set; }

        [Display(Name = "Grupos")]
        public List<string> Roles { get; set; }
    }

    public class InsertUser
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme a senha")]
        [Compare("Password", ErrorMessage = "Senhas não correspondentes")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Grupos")]
        public List<CheckBoxItem> Roles { get; set; }
    }

    public class EditUser
    {
        public EditUser()
        {
            Roles = new List<CheckBoxItem>();
        }
        [Key]
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Grupos")]
        public List<CheckBoxItem> Roles { get; set; }
    }

    public class ChangePassword
    {
        public string Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme a senha")]
        [Compare("NewPassword", ErrorMessage = "Senhas não correspondentes.")]
        public string ConfirmPassword { get; set; }
    }
}
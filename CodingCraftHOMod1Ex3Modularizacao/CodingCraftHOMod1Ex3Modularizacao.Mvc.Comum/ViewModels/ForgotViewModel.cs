﻿using System.ComponentModel.DataAnnotations;

namespace CodingCraftHOMod1Ex3Modularizacao.Mvc.Comum.ViewModels
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}

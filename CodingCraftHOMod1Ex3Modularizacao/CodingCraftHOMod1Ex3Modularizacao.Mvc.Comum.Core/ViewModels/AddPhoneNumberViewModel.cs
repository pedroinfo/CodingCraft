﻿using System.ComponentModel.DataAnnotations;

namespace CodingCraftHOMod1Ex3Modularizacao.ViewModels
{
    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }
}
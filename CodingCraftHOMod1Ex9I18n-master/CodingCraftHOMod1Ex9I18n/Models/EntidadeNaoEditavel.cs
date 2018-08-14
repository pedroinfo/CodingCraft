using CodingCraftHOMod1Ex9I18n.Models.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace CodingCraftHOMod1Ex9I18n.Models
{
    public abstract class EntidadeNaoEditavel : IEntidadeNaoEditavel
    {
        [Display(Name = "[[[DataCriacao]]]")]
        public DateTime DataCriacao { get; set; }
        [Display(Name = "[[[UsuarioCriacao]]]")]
        public string UsuarioCriacao { get; set; }
    }
}
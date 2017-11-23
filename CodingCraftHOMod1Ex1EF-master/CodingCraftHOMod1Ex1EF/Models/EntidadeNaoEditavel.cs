using CodingCraftHOMod1Ex1EF.Models.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace CodingCraftHOMod1Ex1EF.Models
{
    public abstract class EntidadeNaoEditavel : IEntidadeNaoEditavel
    {
        [Display(Name = "Data de Criação")]
        public DateTime DataCriacao { get; set; }
        [Display(Name = "Usuário de Criação")]
        public string UsuarioCriacao { get; set; }
    }
}
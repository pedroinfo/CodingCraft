using CodingCraftHOMod1Ex9I18n.Models.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace CodingCraftHOMod1Ex9I18n.Models
{
    public abstract class Entidade : EntidadeNaoEditavel, IEntidade
    {
        // [Display(Name = "Data da Última Modificação")]
        [Display(Name = nameof(DataUltimaModificacao), ResourceType = typeof(Resources.Linguagem))]
        public DateTime? DataUltimaModificacao { get; set; }
        // [Display(Name = "Usuário da Última Modificação")]
        [Display(Name = nameof(DataUltimaModificacao), ResourceType = typeof(Resources.Linguagem))]
        public string UsuarioUltimaModificacao { get; set; }
    }
}
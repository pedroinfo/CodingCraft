using CodingCraftHOMod1Ex1EF.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CodingCraftHOMod1Ex1EF.Models
{
    public abstract class Entidade : EntidadeNaoEditavel, IEntidade
    {
        [Display(Name = "Data da Última Modificação")]
        public DateTime? DataUltimaModificacao { get; set; }
        [Display(Name = "Usuário da Última Modificação")]
        public string UsuarioUltimaModificacao { get; set; }
    }
}
using System;

namespace CodingCraftHOMod1Ex9I18n.Models.Interfaces
{
    public interface IEntidadeNaoEditavel
    {
        DateTime DataCriacao { get; set; }
        String UsuarioCriacao { get; set; }
    }
}

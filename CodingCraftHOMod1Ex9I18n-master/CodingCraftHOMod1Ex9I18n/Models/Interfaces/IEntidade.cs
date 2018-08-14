using System;

namespace CodingCraftHOMod1Ex9I18n.Models.Interfaces
{
    public interface IEntidade : IEntidadeNaoEditavel
    {
        DateTime? DataUltimaModificacao { get; set; }
        String UsuarioUltimaModificacao { get; set; }
    }
}

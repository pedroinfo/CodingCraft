using System;

namespace CodingCraftHOMod1Ex1EF.Models.Interfaces
{
    public interface IEntidadeNaoEditavel
    {
        DateTime DataCriacao { get; set; }
        String UsuarioCriacao { get; set; }
    }
}

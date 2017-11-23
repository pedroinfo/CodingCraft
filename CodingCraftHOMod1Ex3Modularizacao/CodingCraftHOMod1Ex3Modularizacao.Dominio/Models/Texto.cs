using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodingCraftHOMod1Ex3Modularizacao.Dominio.Models
{
    [Table("Texto")]
    public class Texto
    {
        [Key]
        public int TextoId { get; set; }

        [Display(Name = "Curso")]
        public int CursoId { get; set; } //fk
        public string UserId { get; set; }//fk

        [Display(Name = "Título")]
        public string Titulo { get; set; }

        [Display(Name = "Conteúdo")]
        public string Conteudo { get; set; }

        [Display(Name = "Data de Criação")]
        public DateTime DataHoraTexto { get; set; }

        public bool Revisado { get; set; }

        public bool Aprovado { get; set; }

        public bool Publicado { get; set; }

        [Display(Name = "Curso")]
        public Curso Curso { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}

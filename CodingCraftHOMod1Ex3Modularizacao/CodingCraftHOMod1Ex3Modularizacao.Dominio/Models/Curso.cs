using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingCraftHOMod1Ex3Modularizacao.Dominio.Models
{
    [Table("Curso")]
    public class Curso
    {
        [Key]
        public int CursoId { get; set; }

        [Display(Name = "Nome do curso")]
        public string Nome { get; set; }


    }
}

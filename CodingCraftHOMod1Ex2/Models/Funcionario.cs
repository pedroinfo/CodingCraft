using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EX2.Models
{
    [Table("Funcionario")]
    public class Funcionario
    {
        [Key]
        public int FuncionarioId { get; set; }

        public string Nome { get; set; }
        public string Cargo { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EX11.Models
{
    [Table("Pesquisa")]
    public class Pesquisa
    {
        [Key]
        public int PesquisaId { get; set; }

        public int PaisId { get; set; }
        public int CategoriaId { get; set; }
        public int IndicadorId { get; set; }

        public int Ano { get; set; }

        public Pais Pais { get; set; }
        public Categoria Categoria { get; set; }
        public Indicador Indicador { get; set; }

    }
}
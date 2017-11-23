using EX11.Models;
using EX11.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EX11.ViewModel
{
    public class PesquisaViewModel
    {
        [Display(Name ="Pesquisa")]
        public int? PesquisaId { get; set; }

        [Display(Name = "Indicador")]
        public int? IndicadorId { get; set; }

        [Display(Name = "Categoria")]
        public int? CategoriaId { get; set; }

        [Display(Name= "País")]
        public int? PaisId { get; set; }

        public int? Ano { get; set; }

        [Display(Name = "Formato de Saída")]
        public FormatoSaida FormatoSaida { get; set; }

        public virtual IEnumerable<Pesquisa> Resultados { get; set; }
    }
}
using CodingCraftHOMod1Ex6Dapper.Enums;
using CodingCraftHOMod1Ex6Dapper.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CodingCraftHOMod1Ex6Dapper.ViewModels
{
    public class PesquisaViewModel
    {

        [Display(Name = "Data Inicial")]
        public int DataInicial { get; set; }


        [Display(Name = "Data Final")]
        public int DataFinal { get; set; }

        [Display(Name = "País")]
        public int? PaisId { get; set; }

        [Display(Name = "Tipo de Relatório")]
        public TipoRelatorio TipoRelatorio { get; set; }


        public decimal? Media { get; set; } = 0;

        public double? DesvioPadrao { get; set; } = 0;

        public decimal? Maximo { get; set; } = 0;

        public decimal? Minimo { get; set; } = 0;

        public virtual IEnumerable<AgricultureGdp> Resultados { get; set; }

    }
}
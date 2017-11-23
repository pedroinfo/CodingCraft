using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CodingCraftHOMod1Ex1EF.Models
{
    public class Vendas
    {
        [Key]
        public int VendaId { get; set; }
        public int ProdutoId { get; set; }
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "Informe a data da venda")]
        [DataType(DataType.Date)]
        public DateTime DataCompra { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal ValorVenda { get; set; }

        public virtual Produto Produto { get; set; }
        public virtual Clientes Cliente { get; set; }

    }
}
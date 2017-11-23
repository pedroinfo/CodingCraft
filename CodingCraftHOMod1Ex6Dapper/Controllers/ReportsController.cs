using CodingCraftHOMod1Ex6Dapper.Models;
using System.Web.Mvc;
using Dapper;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using CodingCraftHOMod1Ex6Dapper.ViewModels;
using System;
using CodingCraftHOMod1Ex6Dapper.Extension;

namespace CodingCraftHOMod1Ex6Dapper.Controllers
{
    public class ReportsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(PesquisaViewModel pesquisaViewModel = null)
        {
            var paises = db.Database.Connection.Query<Country>("select * from Countries");

            SelectList selectPaises = new SelectList(paises, "CountryId", "Name");
            ViewBag.PaisId = selectPaises;

            string relatorio = Request.QueryString["TipoRelatorio"];
            string pais = Request.QueryString["PaisId"];
            string dataInicio = Request.QueryString["DataInicial"];
            string dataFim = Request.QueryString["DataFinal"];

            if (pesquisaViewModel.DataInicial < 1960)
                pesquisaViewModel.DataInicial = 1960;

            if (pesquisaViewModel.DataFinal < 1960)
                pesquisaViewModel.DataFinal = 2017;
            
            if (!string.IsNullOrEmpty(relatorio) && 
                !string.IsNullOrEmpty(pais) && 
                !string.IsNullOrEmpty(dataInicio) && 
                !string.IsNullOrEmpty(dataFim))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(" SELECT a.CountryId, a.AgricultureGdpId, a.Year, a.Value, b.CountryId, b.Name FROM AgricultureGdps a ");
                sb.Append(" INNER JOIN Countries b ON a.CountryId = b.CountryId ");
                sb.Append(" WHERE a.CountryId = @pais AND Year BETWEEN @dataInicio AND @dataFim ");


                var parametros = new DynamicParameters();
                parametros.Add("@pais", pais);
                parametros.Add("@dataInicio", dataInicio);
                parametros.Add("@dataFim", dataFim);

                var registros = db.Database.Connection.Query<AgricultureGdp, Country, AgricultureGdp>
                   (sb.ToString(), (agricultureGdp, country) =>
                   {
                       agricultureGdp.CountryId = country.CountryId;
                       return agricultureGdp;
                   }, splitOn: "CountryId", param: parametros);


                pesquisaViewModel.Resultados = registros.ToList(); 

                /*Calculos*/

                pesquisaViewModel.Media = registros.Sum(x => x.Value) / registros.Count();
                pesquisaViewModel.DesvioPadrao = registros.Select(x => Convert.ToDouble(x.Value)).StandardDeviation();
                pesquisaViewModel.Minimo = registros.Min(x=>x.Value).Value;
                pesquisaViewModel.Maximo = registros.Max(x=>x.Value).Value;

                return View(pesquisaViewModel);
            }
            
            return View(pesquisaViewModel);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }

}
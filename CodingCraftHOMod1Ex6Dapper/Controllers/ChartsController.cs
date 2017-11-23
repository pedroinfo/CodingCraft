using System.Web.Mvc;
using CodingCraftHOMod1Ex6Dapper.Models;
using Dapper;
using System.Web.Helpers;
using CodingCraftHOMod1Ex6Dapper.ViewModels;
using System.Text;
using System.Linq;
using System;
using CodingCraftHOMod1Ex6Dapper.Extension;

namespace CodingCraftHOMod1Ex6Dapper.Controllers
{
    public class ChartsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(PesquisaViewModel pesquisaViewModel = null)
        {
            var paises = db.Database.Connection.Query<Country>("select * from Countries");
            
            SelectList selectPaises = new SelectList(paises, "CountryId", "Name");
            ViewBag.PaisId = selectPaises;


            ViewBag.MobileCount_List = new[] { "abc", "bbb", "ccc" };
            ViewBag.Productname_List = new[] { "2", "3", "4" };

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

                ViewBag.Keys = registros.Select(x => x.Year).ToArray();
                ViewBag.Values = registros.Select(x => x.Value).ToArray();


                return View(pesquisaViewModel);
            }

            return View(pesquisaViewModel);
        }



        /*
        public ActionResult EfficiencyChart(string pid)
        {
            var myChart = new Chart(width: 1000, height: 600)
            .AddTitle("Employee's Efficiency")
            .AddSeries(
                name: "Employee",
                xValue: t2,
                yValues: t1)
            .Write();
            return base.File("~/Content/chart" + user.Id, "jpeg");
        }*/
    }
}
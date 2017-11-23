using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EX11.Models;
using EX11.ViewModel;
using X.PagedList;
using Newtonsoft.Json;
using EX11.Models.Enums;
using CsvHelper;
using System.IO;
using System.Text;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace EX11.Controllers
{
    public class PesquisaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        protected async Task<IPagedList<Pesquisa>> Consulta(int? page, PesquisaViewModel viewModel = null)
        {
            var consulta = db.Pesquisas.Include(p => p.Pais).Include(c=> c.Categoria).Include(i=> i.Indicador);

            if (viewModel.IndicadorId != null)
                consulta = consulta.Where(x => x.IndicadorId == viewModel.IndicadorId);

            if (viewModel.CategoriaId != null)
                consulta = consulta.Where(x => x.CategoriaId == viewModel.CategoriaId);

            if (viewModel.PaisId != null)
                consulta = consulta.Where(x => x.PaisId == viewModel.PaisId);

            if (viewModel.Ano != null)
                consulta = consulta.Where(x => x.Ano == viewModel.Ano);

            consulta = consulta.OrderBy(x => x.PaisId);

            var pageNumber = page ?? 1;

            return await consulta.ToPagedListAsync(pageNumber, 10);
        }

        public async Task<ActionResult> Index(int? page, PesquisaViewModel viewModel = null)
        {
            ViewBag.PaisId = new SelectList(db.Pais, "PaisId", "Nome");
            ViewBag.CategoriaId = new SelectList(db.Categoria, "CategoriaId", "Nome");
            ViewBag.IndicadorId = new SelectList(db.Indicador, "IndicadorId", "Nome");

            viewModel.Resultados = await Consulta(page, viewModel);

            var modeloExport = viewModel.Resultados.Select(x => new
            {
                Indicador = x.Indicador.Nome,
                Categoria = x.Categoria.Nome,
                Pais = x.Pais.Nome,
                Ano = x.Ano
            }).ToList();

            switch (viewModel.FormatoSaida)
            {
                case FormatoSaida.Csv:
                    var stringWriter = new StringWriter();
                    var csv = new CsvWriter(stringWriter);
                    csv.WriteRecords(modeloExport);
                    var str = stringWriter.ToString();
                    return File(new MemoryStream(Encoding.UTF8.GetBytes(str ?? "")), "text/csv", "Relatorio.csv");

                case FormatoSaida.Excel:
                    ListToExcel(modeloExport);
                    return View(viewModel);

                case FormatoSaida.Json:
                    var json = JsonConvert.SerializeObject(modeloExport);
                    return File(new UTF8Encoding().GetBytes(json), "text/json", "Relatorio.json");

                case FormatoSaida.Tela:
                default:
                    return View(viewModel);
            }
        }

        void ListToExcel<T>(List<T> query)
        {
            using (ExcelPackage pck = new ExcelPackage())
            {
                //Create the worksheet
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Consulta");

                //get our column headings
                var t = typeof(T);
                var Headings = t.GetProperties();
                for (int i = 0; i < Headings.Count(); i++)
                {

                    ws.Cells[1, i + 1].Value = Headings[i].Name;
                }
                
                if (query.Count() > 0)
                {
                    ws.Cells["A2"].LoadFromCollection(query);
                }

                using (ExcelRange rng = ws.Cells["A1:BZ1"])
                {
                    rng.Style.Font.Bold = true;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;                     
                    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189));  
                    rng.Style.Font.Color.SetColor(Color.White);
                }

                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=ExcelDemo.xlsx");
                Response.BinaryWrite(pck.GetAsByteArray());
                Response.End();
            }
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

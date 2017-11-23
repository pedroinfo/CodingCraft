using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CodingCraftHOMod1Ex5WebAPI.Models;
using CodingCraftHOMod1Ex5WebAPI.Models.Contexto;

namespace CodingCraftHOMod1Ex5WebAPI.Controllers
{
    public class ListagemController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Listagem
        public async Task<ActionResult> Index()
        {
            var arquivos = db.Arquivos.Include(a => a.ArquivoTipo);
            return View(await arquivos.ToListAsync());
        }

        // GET: Listagem/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Arquivo arquivo = await db.Arquivos.FindAsync(id);
            if (arquivo == null)
            {
                return HttpNotFound();
            }
            return View(arquivo);
        }

        // GET: Listagem/Create
        public ActionResult Create()
        {
            ViewBag.ArquivoTipoId = new SelectList(db.ArquivosTipos, "ArquivoTipoId", "Descricao");
            return View();
        }

        // POST: Listagem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ArquivoId,ArquivoTipoId,NomeArquivo,DataEntrada")] Arquivo arquivo)
        {
            if (ModelState.IsValid)
            {
                db.Arquivos.Add(arquivo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ArquivoTipoId = new SelectList(db.ArquivosTipos, "ArquivoTipoId", "Descricao", arquivo.ArquivoTipoId);
            return View(arquivo);
        }

        // GET: Listagem/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Arquivo arquivo = await db.Arquivos.FindAsync(id);
            if (arquivo == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArquivoTipoId = new SelectList(db.ArquivosTipos, "ArquivoTipoId", "Descricao", arquivo.ArquivoTipoId);
            return View(arquivo);
        }

        // POST: Listagem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ArquivoId,ArquivoTipoId,NomeArquivo,DataEntrada")] Arquivo arquivo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(arquivo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ArquivoTipoId = new SelectList(db.ArquivosTipos, "ArquivoTipoId", "Descricao", arquivo.ArquivoTipoId);
            return View(arquivo);
        }

        // GET: Listagem/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Arquivo arquivo = await db.Arquivos.FindAsync(id);
            if (arquivo == null)
            {
                return HttpNotFound();
            }
            return View(arquivo);
        }

        // POST: Listagem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Arquivo arquivo = await db.Arquivos.FindAsync(id);
            db.Arquivos.Remove(arquivo);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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

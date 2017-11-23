using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CodingCraftHOMod1Ex7Redis.Models;
using X.PagedList;

namespace CodingCraftHOMod1Ex7Redis.Controllers
{
    public class EmpresasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Empresas
        public async Task<ActionResult> Index(int? page)
        {
            if (!await RedisCacheClient.ExistsAsync("Empresa"))
            {
                await RedisCacheClient.AddAsync("Empresa", "", new TimeSpan(0, 3, 0));

                var empresaDB = await db.Empresas.ToListAsync();

                foreach (var item in empresaDB)
                {
                    await RedisCacheClient.AddAsync("Empresa:" + item.EmpresaId.ToString(), item, new TimeSpan(0, 3, 0));
                }
            }

            var empresas = (await RedisCacheClient.SearchKeysAsync("Empresa:*"))
                                                  .Select(p => RedisCacheClient.Get<Empresa>(p)).OrderBy(x => x.EmpresaId).ToList() ??
                                                  await db.Empresas.OrderBy(x => x.EmpresaId).ToListAsync();

            var pageNumber = page ?? 1;
            var paginacao = await empresas.ToPagedListAsync(pageNumber, 25);

            ViewBag.PageEmpresas = paginacao;

            return View();

            
        }

        // GET: Empresas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empresa empresa = await RedisCacheClient.GetAsync<Empresa>("Empresa:" + id) ??
                                      await db.Empresas.FindAsync(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            return View(empresa);
        }

        // GET: Empresas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Empresas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "EmpresaId,RazaoSocial,NomeFantasia,Cnpj")] Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                db.Empresas.Add(empresa);
                await db.SaveChangesAsync();
                await RedisCacheClient.AddAsync("Empresa:" + empresa.EmpresaId, empresa, new TimeSpan(0, 10, 0));
                return RedirectToAction("Index");
            }

            return View(empresa);
        }

        // GET: Empresas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empresa empresa = await RedisCacheClient.GetAsync<Empresa>("Empresa:" + id) ??
                                      await db.Empresas.FindAsync(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            return View(empresa);
        }

        // POST: Empresas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "EmpresaId,RazaoSocial,NomeFantasia,Cnpj")] Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empresa).State = EntityState.Modified;
                await db.SaveChangesAsync();
                await RedisCacheClient.ReplaceAsync("Empresa:" + empresa.EmpresaId, empresa, new TimeSpan(0, 10, 0));
                return RedirectToAction("Index");
            }
            return View(empresa);
        }

        // GET: Empresas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           // Empresa empresa = await db.Empresas.FindAsync(id);

            Empresa empresa = await RedisCacheClient.GetAsync<Empresa>("Empresa:" + id) ??
                                      await db.Empresas.FindAsync(id);

            if (empresa == null)
            {
                return HttpNotFound();
            }
            return View(empresa);
        }

        // POST: Empresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Empresa empresa = await db.Empresas.FindAsync(id);
            db.Empresas.Remove(empresa);
            await db.SaveChangesAsync();
            await RedisCacheClient.RemoveAsync("Empresa:" + id);
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

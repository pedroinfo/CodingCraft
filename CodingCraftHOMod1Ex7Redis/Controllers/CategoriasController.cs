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
    public class CategoriasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Categorias
        public async Task<ActionResult> Index(int? page)
        {
            if (!await RedisCacheClient.ExistsAsync("Categoria"))
            {
                await RedisCacheClient.AddAsync("Categoria", "", new TimeSpan(0, 3, 0));

                var categoriaDB = await db.Categorias.ToListAsync();

                foreach (var item in categoriaDB)
                {
                    await RedisCacheClient.AddAsync("Categoria:" + item.CategoriaId.ToString(), item, new TimeSpan(0, 3, 0));
                }
            }

            var categorias = (await RedisCacheClient.SearchKeysAsync("Categoria:*"))
                                                  .Select(p => RedisCacheClient.Get<Categoria>(p)).OrderBy(x => x.CategoriaId).ToList() ??
                                                  await db.Categorias.OrderBy(x => x.CategoriaId).ToListAsync();

            var pageNumber = page ?? 1;
            var paginacao = await categorias.ToPagedListAsync(pageNumber, 25);

            ViewBag.PageCategorias = paginacao;

            return View();
        }

        // GET: Categorias/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Categoria categoria = await RedisCacheClient.GetAsync<Categoria>("Categoria:" + id) ??
                                      await db.Categorias.FindAsync(id);

            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // GET: Categorias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categorias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CategoriaId,Nome")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                db.Categorias.Add(categoria);
                await db.SaveChangesAsync();
                await RedisCacheClient.AddAsync("Categoria:" + categoria.CategoriaId, categoria, new TimeSpan(0, 10, 0));
                return RedirectToAction("Index");
            }

            return View(categoria);
        }

        // GET: Categorias/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = await RedisCacheClient.GetAsync<Categoria>("Categoria:" + id) ??
                                      await db.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // POST: Categorias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CategoriaId,Nome")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoria).State = EntityState.Modified;
                await db.SaveChangesAsync();
                await RedisCacheClient.ReplaceAsync("Categoria:" + categoria.CategoriaId, categoria, new TimeSpan(0, 10, 0));

                return RedirectToAction("Index");
            }
            return View(categoria);
        }

        // GET: Categorias/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = await RedisCacheClient.GetAsync<Categoria>("Categoria:" + id) ??
                                      await db.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // POST: Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Categoria categoria = await db.Categorias.FindAsync(id);
            db.Categorias.Remove(categoria);
            await db.SaveChangesAsync();

            await RedisCacheClient.RemoveAsync("Categoria:" + id);
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

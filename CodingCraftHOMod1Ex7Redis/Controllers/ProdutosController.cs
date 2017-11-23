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
    public class ProdutosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Produtos
        public async Task<ActionResult> Index(int? page)
        {
            if (!await RedisCacheClient.ExistsAsync("Produtos"))
            {
                await RedisCacheClient.AddAsync("Produto", "", new TimeSpan(0, 3, 0));

                var produtosDB = await db.Produtos.ToListAsync();

                foreach (var item in produtosDB)
                {
                    await RedisCacheClient.AddAsync("Produto:" + item.ProdutoId.ToString(), item, new TimeSpan(0, 3, 0));
                }
            }

            var produtos = (await RedisCacheClient.SearchKeysAsync("Produto:*"))
                                                  .Select(p => RedisCacheClient.Get<Produto>(p)).OrderBy(x => x.ProdutoId).ToList() ??
                                                  await db.Produtos.OrderBy(x => x.ProdutoId).ToListAsync();

            var pageNumber = page ?? 1;
            var paginacao = await produtos.ToPagedListAsync(pageNumber, 25);

            ViewBag.PageProdutos = paginacao;

            return View();
        }

        // GET: Produtos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Produto produto = await RedisCacheClient.GetAsync<Produto>("Produto:" + id) ??
                                      await db.Produtos.FindAsync(id);

            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        // GET: Produtos/Create
        public ActionResult Create()
        {
            ViewBag.CategoriaId = new SelectList(db.Categorias, "CategoriaId", "Nome");
            return View();
        }

        // POST: Produtos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProdutoId,CategoriaId,Nome,Preco")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                db.Produtos.Add(produto);
                await db.SaveChangesAsync();
                await RedisCacheClient.AddAsync("Produto:" + produto.ProdutoId, produto, new TimeSpan(0, 10, 0));
                return RedirectToAction("Index");
            }

            ViewBag.CategoriaId = new SelectList(db.Categorias, "CategoriaId", "Nome", produto.CategoriaId);
            return View(produto);
        }

        // GET: Produtos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = await RedisCacheClient.GetAsync<Produto>("Produto:" + id) ??
                                      await db.Produtos.FindAsync(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriaId = new SelectList(db.Categorias, "CategoriaId", "Nome", produto.CategoriaId);
            return View(produto);
        }

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProdutoId,CategoriaId,Nome,Preco")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(produto).State = EntityState.Modified;
                await db.SaveChangesAsync();
                await RedisCacheClient.ReplaceAsync("Produto:" + produto.ProdutoId, produto, new TimeSpan(0, 10, 0));
                return RedirectToAction("Index");
            }
            ViewBag.CategoriaId = new SelectList(db.Categorias, "CategoriaId", "Nome", produto.CategoriaId);
            return View(produto);
        }

        // GET: Produtos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = await RedisCacheClient.GetAsync<Produto>("Produto:" + id) ??
                                      await db.Produtos.FindAsync(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Produto produto = await db.Produtos.FindAsync(id);
            db.Produtos.Remove(produto);
            await db.SaveChangesAsync();

            await RedisCacheClient.RemoveAsync("Produto:" + id);
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

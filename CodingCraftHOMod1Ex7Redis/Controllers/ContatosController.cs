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
    public class ContatosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Contatos
        public async Task<ActionResult> Index(int? page)
        {
            if (!await RedisCacheClient.ExistsAsync("Contato"))
            {
                await RedisCacheClient.AddAsync("Contato", "", new TimeSpan(0, 3, 0));

                var contatoDB = await db.Contatos.ToListAsync();

                foreach (var item in contatoDB)
                {
                    await RedisCacheClient.AddAsync("Contato:" + item.ContatoId.ToString(), item, new TimeSpan(0, 3, 0));
                }
            }

            var contatos = (await RedisCacheClient.SearchKeysAsync("Contato:*"))
                                                  .Select(p => RedisCacheClient.Get<Contato>(p)).OrderBy(x => x.ContatoId).ToList() ??
                                                  await db.Contatos.OrderBy(x => x.ContatoId).ToListAsync();

            var pageNumber = page ?? 1;
            var paginacao = await contatos.ToPagedListAsync(pageNumber, 25);

            ViewBag.PageContatos = paginacao;

            return View();
        }

        // GET: Contatos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contato contato = await RedisCacheClient.GetAsync<Contato>("Contato:" + id) ??
                                      await db.Contatos.FindAsync(id);

            if (contato == null)
            {
                return HttpNotFound();
            }
            return View(contato);
        }

        // GET: Contatos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contatos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ContatoId,Nome,Email")] Contato contato)
        {
            if (ModelState.IsValid)
            {
                db.Contatos.Add(contato);
                await db.SaveChangesAsync();
                await RedisCacheClient.AddAsync("Contato:" + contato.ContatoId, contato, new TimeSpan(0, 10, 0));
                return RedirectToAction("Index");
            }

            return View(contato);
        }

        // GET: Contatos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Contato contato = await RedisCacheClient.GetAsync<Contato>("Contato:" + id) ??
                                     await db.Contatos.FindAsync(id);

            if (contato == null)
            {
                return HttpNotFound();
            }
            return View(contato);
        }

        // POST: Contatos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ContatoId,Nome,Email")] Contato contato)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contato).State = EntityState.Modified;
                await db.SaveChangesAsync();
                await RedisCacheClient.ReplaceAsync("Contato:" + contato.ContatoId, contato, new TimeSpan(0, 10, 0));
                return RedirectToAction("Index");
            }
            return View(contato);
        }

        // GET: Contatos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Contato contato = await RedisCacheClient.GetAsync<Contato>("Contato:" + id) ??
                                      await db.Contatos.FindAsync(id);

            if (contato == null)
            {
                return HttpNotFound();
            }
            return View(contato);
        }

        // POST: Contatos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Contato contato = await db.Contatos.FindAsync(id);
            db.Contatos.Remove(contato);
            await db.SaveChangesAsync();
            await RedisCacheClient.RemoveAsync("Contato:" + id);
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

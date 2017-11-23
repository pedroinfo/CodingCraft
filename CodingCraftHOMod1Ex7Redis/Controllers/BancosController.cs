using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using CodingCraftHOMod1Ex7Redis.Models;
using System.Linq;
using X.PagedList;
using System;
using System.Collections.Generic;

namespace CodingCraftHOMod1Ex7Redis.Controllers
{
    public class BancosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Bancos
        public async Task<ActionResult> Index(int? page)
        {
            if (!await RedisCacheClient.ExistsAsync("Banco"))
            {
                await RedisCacheClient.AddAsync("Banco", "", new TimeSpan(0, 3, 0));

                var compromissoDB = await db.Bancos.ToListAsync();

                foreach (var item in compromissoDB)
                {
                    await RedisCacheClient.AddAsync("Banco:" + item.BancoId.ToString(), item, new TimeSpan(0, 3, 0));
                }
            }

            var bancos = (await RedisCacheClient.SearchKeysAsync("Banco:*"))
                                                  .Select(p => RedisCacheClient.Get<Banco>(p)).OrderBy(x => x.BancoId).ToList() ??
                                                  await db.Bancos.OrderBy(x => x.BancoId).ToListAsync();

            var pageNumber = page ?? 1;
            var paginacao = await bancos.ToPagedListAsync(pageNumber, 25);

            ViewBag.PageBancos = paginacao;
            
            return View();
        }

        // GET: Bancos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
          //  Banco banco = await db.Bancos.FindAsync(id);

            Banco banco = await RedisCacheClient.GetAsync<Banco>("Banco:" + id) ?? await db.Bancos.FindAsync(id);
            
            if (banco == null)
            {
                return HttpNotFound();
            }
            return View(banco);
        }

        // GET: Bancos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bancos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "BancoId,Nome,Numero")] Banco banco)
        {
            if (ModelState.IsValid)
            {
                db.Bancos.Add(banco);
                await db.SaveChangesAsync();
                await RedisCacheClient.AddAsync("Banco:" + banco.BancoId, banco, new TimeSpan(0, 10, 0));
                return RedirectToAction("Index");
            }

            return View(banco);
        }

        // GET: Bancos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banco banco = await RedisCacheClient.GetAsync<Banco>("Banco:" + id);
  
            if (banco == null)
            {
                return HttpNotFound();
            }
            return View(banco);
        }

        // POST: Bancos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "BancoId,Nome,Numero")] Banco banco)
        {
            if (ModelState.IsValid)
            {
                db.Entry(banco).State = EntityState.Modified;
                await db.SaveChangesAsync();
                await RedisCacheClient.ReplaceAsync("Banco:" + banco.BancoId, banco, new TimeSpan(0, 10, 0));

                return RedirectToAction("Index");
            }
            return View(banco);
        }

        // GET: Bancos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banco banco = await RedisCacheClient.GetAsync<Banco>("Banco:" + id) ?? await db.Bancos.FindAsync(id);
            if (banco == null)
            {
                return HttpNotFound();
            }
            return View(banco);
        }

        // POST: Bancos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Banco banco = await db.Bancos.FindAsync(id);
            db.Bancos.Remove(banco);
            await db.SaveChangesAsync();
            await RedisCacheClient.RemoveAsync("Banco:" + banco.BancoId);
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using CodingCraftHOMod1Ex7Redis.Models;
using X.PagedList;

namespace CodingCraftHOMod1Ex7Redis.Controllers
{
    public class CompromissosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Compromissos
        public async Task<ActionResult> Index(int? page)
        {
            if (!await RedisCacheClient.ExistsAsync("Compromisso"))
            {
                    await RedisCacheClient.AddAsync("Compromisso", "", new TimeSpan(0, 3, 0));

                var compromissoDB = await db.Compromissos.ToListAsync();

                foreach (var item in compromissoDB)
                {
                    await RedisCacheClient.AddAsync("Compromisso:" + item.CompromissoId.ToString(), item, new TimeSpan(0, 3, 0));
                }
            }
            
            var compromissos = (await RedisCacheClient.SearchKeysAsync("Compromisso:*"))
                                                  .Select(p => RedisCacheClient.Get<Compromisso>(p)).OrderBy(x => x.CompromissoId).ToList() ??
                                                  await db.Compromissos.OrderBy(x=>x.CompromissoId).ToListAsync();

            var pageNumber = page ?? 1; 
            var paginacao = await compromissos.ToPagedListAsync(pageNumber, 25); 

            ViewBag.PageCompromissos = paginacao;

            return View();
        }

        [HttpGet]
        public async Task<ActionResult> BuscarCompromisso(string compromisso)
        {
            
            if (String.IsNullOrEmpty(compromisso)) return Json(null, JsonRequestBehavior.AllowGet);

            if (!await RedisCacheClient.ExistsAsync("Compromissos"))
            {
                var compromissoDB = await db.Compromissos.ToListAsync();

                await RedisCacheClient.AddAsync("Compromissos", compromissoDB, new TimeSpan(0, 3, 0));
            }

            var lista = await RedisCacheClient.GetAsync<List<Compromisso>>("Compromissos");
            return Json(lista
                .Where(a => a.Titulo.Contains(compromisso)), JsonRequestBehavior.AllowGet);

    
        }

        // GET: Compromissos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compromisso compromisso = await RedisCacheClient.GetAsync<Compromisso>("Compromisso:" + id) ??
                                      await db.Compromissos.FindAsync(id);


            if (compromisso == null)
            {
                return HttpNotFound();
            }
            return View(compromisso);
        }

        // GET: Compromissos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Compromissos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CompromissoId,Titulo,DataHora,Local,Observacoes")] Compromisso compromisso)
        {
            if (ModelState.IsValid)
            {
                db.Compromissos.Add(compromisso);
                await db.SaveChangesAsync();
                await RedisCacheClient.AddAsync("Compromisso:" + compromisso.CompromissoId, compromisso, new TimeSpan(0, 10, 0));
                return RedirectToAction("Index");
            }

            return View(compromisso);
        }

        // GET: Compromissos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compromisso compromisso = await RedisCacheClient.GetAsync<Compromisso>("Compromisso:" + id) ??
                                      await db.Compromissos.FindAsync(id);

            if (compromisso == null)
            {
                return HttpNotFound();
            }
            return View(compromisso);
        }

        // POST: Compromissos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CompromissoId,Titulo,DataHora,Local,Observacoes")] Compromisso compromisso)
        {
            if (ModelState.IsValid)
            {
                db.Entry(compromisso).State = EntityState.Modified;
                await db.SaveChangesAsync();
                await RedisCacheClient.ReplaceAsync("Compromisso:" + compromisso.CompromissoId, compromisso, new TimeSpan(0, 10, 0));

                return RedirectToAction("Index");
            }
            return View(compromisso);
        }

        // GET: Compromissos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compromisso compromisso = await RedisCacheClient.GetAsync<Compromisso>("Compromisso:" + id) ??
                                      await db.Compromissos.FindAsync(id);
            if (compromisso == null)
            {
                return HttpNotFound();
            }
            return View(compromisso);
        }

        // POST: Compromissos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Compromisso compromisso = await db.Compromissos.FindAsync(id);
            db.Compromissos.Remove(compromisso);
            await db.SaveChangesAsync();

            await RedisCacheClient.RemoveAsync("Compromisso:" + id);
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

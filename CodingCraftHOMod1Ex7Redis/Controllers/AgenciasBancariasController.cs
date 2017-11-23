using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using CodingCraftHOMod1Ex7Redis.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Transactions;

namespace CodingCraftHOMod1Ex7Redis.Controllers
{
    public class AgenciasBancariasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AgenciasBancarias
        public async Task<ActionResult> Index()
        {
            var agenciasBancarias = db.AgenciasBancarias.Include(a => a.Banco);
            return View(await agenciasBancarias.ToListAsync());
        }

        // GET: AgenciasBancarias/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgenciaBancaria agenciaBancaria = await RedisCacheClient.GetAsync<AgenciaBancaria>("AgenciaBancaria:" + id) ?? await db.AgenciasBancarias.FindAsync(id);
            if (agenciaBancaria == null)
            {
                return HttpNotFound();
            }
            return View(agenciaBancaria);
        }

        // GET: AgenciasBancarias/Create
        public ActionResult Create()
        {
            ViewBag.BancoId = new SelectList(db.Bancos, "BancoId", "Nome");
            return View();
        }

        // POST: AgenciasBancarias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AgenciaBancariaId,BancoId,CodigoCompensacao,Nome")] AgenciaBancaria agenciaBancaria)
        {
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    db.AgenciasBancarias.Add(agenciaBancaria);
                    await db.SaveChangesAsync();

                    await RedisCacheClient.AddAsync("AgenciaBancaria:" + agenciaBancaria.AgenciaBancariaId, agenciaBancaria, new TimeSpan(0, 5, 0));

                    scope.Complete();
                }

                return RedirectToAction("Index");
            }

            ViewBag.BancoId = new SelectList(db.Bancos, "BancoId", "Nome", agenciaBancaria.BancoId);
            return View(agenciaBancaria);
        }

        // GET: AgenciasBancarias/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgenciaBancaria agenciaBancaria = await db.AgenciasBancarias.FindAsync(id);
            if (agenciaBancaria == null)
            {
                return HttpNotFound();
            }
            ViewBag.BancoId = new SelectList(db.Bancos, "BancoId", "Nome", agenciaBancaria.BancoId);
            return View(agenciaBancaria);
        }

        // POST: AgenciasBancarias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AgenciaBancariaId,BancoId,CodigoCompensacao,Nome")] AgenciaBancaria agenciaBancaria)
        {
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    db.Entry(agenciaBancaria).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    
                    await RedisCacheClient.ReplaceAsync("AgenciaBancaria:" + agenciaBancaria.AgenciaBancariaId, agenciaBancaria, new TimeSpan(0, 5, 0));

                    scope.Complete();
                    return RedirectToAction("Index");
                }    
            }

            ViewBag.BancoId = new SelectList(db.Bancos, "BancoId", "Nome", agenciaBancaria.BancoId);
            return View(agenciaBancaria);
        }

        // GET: AgenciasBancarias/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgenciaBancaria agenciaBancaria = await db.AgenciasBancarias.FindAsync(id);
            if (agenciaBancaria == null)
            {
                return HttpNotFound();
            }
            return View(agenciaBancaria);
        }

        // POST: AgenciasBancarias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                AgenciaBancaria agenciaBancaria = await db.AgenciasBancarias.FindAsync(id);
                db.AgenciasBancarias.Remove(agenciaBancaria);
                await db.SaveChangesAsync();

                await RedisCacheClient.RemoveAsync("AgenciaBancaria:" + id);

                scope.Complete();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<JsonResult> PesquisarJson(String termo)
        {
            db.Configuration.ProxyCreationEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;

            if (String.IsNullOrEmpty(termo)) return Json(null, JsonRequestBehavior.AllowGet);

            if (!await RedisCacheClient.ExistsAsync("Agencias"))
            {
                var agenciasBancarias = await db.AgenciasBancarias
                                      // .Where(a => a.Nome.Contains(termo) || termo.Contains(a.CodigoCompensacao.ToString()))
                                      .ToListAsync();

                await RedisCacheClient.AddAsync("Agencias", agenciasBancarias, new TimeSpan(0, 3, 0));
            }

            termo = termo.ToUpper();
            var lista = await RedisCacheClient.GetAsync<List<AgenciaBancaria>>("Agencias");
            return Json(lista
                .Where(a => a.Nome.Contains(termo) || termo.Contains(a.CodigoCompensacao.ToString())), JsonRequestBehavior.AllowGet);

            // return Json(agenciasBancarias, JsonRequestBehavior.AllowGet);
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

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
    public class FuncionariosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Funcionarios
        public async Task<ActionResult> Index(int? page)
        {
            if (!await RedisCacheClient.ExistsAsync("Funcionario"))
            {
                await RedisCacheClient.AddAsync("Funcionario", "", new TimeSpan(0, 3, 0));

                var funcionarioDB = await db.Funcionarios.ToListAsync();

                foreach (var item in funcionarioDB)
                {
                    await RedisCacheClient.AddAsync("Funcionario:" + item.FuncionarioId.ToString(), item, new TimeSpan(0, 3, 0));
                }
            }

            var funcionarios = (await RedisCacheClient.SearchKeysAsync("Funcionario:*"))
                                                  .Select(p => RedisCacheClient.Get<Funcionario>(p)).OrderBy(x => x.FuncionarioId).ToList() ??
                                                  await db.Funcionarios.OrderBy(x => x.FuncionarioId).ToListAsync();

            var pageNumber = page ?? 1;
            var paginacao = await funcionarios.ToPagedListAsync(pageNumber, 25);

            ViewBag.PageFuncionarios = paginacao;

            return View();
            
        }

        // GET: Funcionarios/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Funcionario funcionario = await RedisCacheClient.GetAsync<Funcionario>("Funcionario:" + id) ??
                                      await db.Funcionarios.FindAsync(id);

            if (funcionario == null)
            {
                return HttpNotFound();
            }
            return View(funcionario);
        }

        // GET: Funcionarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Funcionarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "FuncionarioId,Nome,Cargo,DataNascimento")] Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                db.Funcionarios.Add(funcionario);
                await db.SaveChangesAsync();
                await RedisCacheClient.AddAsync("Funcionario:" + funcionario.FuncionarioId, funcionario, new TimeSpan(0, 10, 0));
                return RedirectToAction("Index");
            }

            return View(funcionario);
        }

        // GET: Funcionarios/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
          
            Funcionario funcionario = await RedisCacheClient.GetAsync<Funcionario>("Funcionario:" + id) ??
                          await db.Funcionarios.FindAsync(id);


            if (funcionario == null)
            {
                return HttpNotFound();
            }
            return View(funcionario);
        }

        // POST: Funcionarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "FuncionarioId,Nome,Cargo,DataNascimento")] Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(funcionario).State = EntityState.Modified;
                await db.SaveChangesAsync();
                await RedisCacheClient.ReplaceAsync("Funcionario:" + funcionario.FuncionarioId, funcionario, new TimeSpan(0, 10, 0));
                return RedirectToAction("Index");
            }
            return View(funcionario);
        }

        // GET: Funcionarios/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Funcionario funcionario = await RedisCacheClient.GetAsync<Funcionario>("Funcionario:" + id) ??
                          await db.Funcionarios.FindAsync(id);


            if (funcionario == null)
            {
                return HttpNotFound();
            }
            return View(funcionario);
        }

        // POST: Funcionarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Funcionario funcionario = await db.Funcionarios.FindAsync(id);
            db.Funcionarios.Remove(funcionario);
            await db.SaveChangesAsync();
            await RedisCacheClient.RemoveAsync("Funcionario:" + id);
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

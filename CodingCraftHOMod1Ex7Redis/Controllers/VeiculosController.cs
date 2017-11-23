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
    public class VeiculosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Veiculos
        public async Task<ActionResult> Index(int? page)
        {
            if (!await RedisCacheClient.ExistsAsync("Veiculos"))
            {
                await RedisCacheClient.AddAsync("Veiculo", "", new TimeSpan(0, 3, 0));

                var veiculosDB = await db.Veiculos.ToListAsync();

                foreach (var item in veiculosDB)
                {
                    await RedisCacheClient.AddAsync("Veiculo:" + item.VeiculoId.ToString(), item, new TimeSpan(0, 3, 0));
                }
            }

            var veiculos = (await RedisCacheClient.SearchKeysAsync("Veiculo:*"))
                                                  .Select(p => RedisCacheClient.Get<Veiculo>(p)).OrderBy(x => x.VeiculoId).ToList() ??
                                                  await db.Veiculos.OrderBy(x => x.VeiculoId).ToListAsync();

            var pageNumber = page ?? 1;
            var paginacao = await veiculos.ToPagedListAsync(pageNumber, 25);

            ViewBag.PageVeiculos = paginacao;

            return View();
        }

        // GET: Veiculos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Veiculo veiculo = await RedisCacheClient.GetAsync<Veiculo>("Veiculo:" + id) ??
                                      await db.Veiculos.FindAsync(id);
            
            if (veiculo == null)
            {
                return HttpNotFound();
            }
            return View(veiculo);
        }

        // GET: Veiculos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Veiculos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "VeiculoId,Carro,Marca,Ano,Placa")] Veiculo veiculo)
        {
            if (ModelState.IsValid)
            {
                db.Veiculos.Add(veiculo);
                await db.SaveChangesAsync();
                await RedisCacheClient.AddAsync("Veiculo:" + veiculo.VeiculoId, veiculo, new TimeSpan(0, 10, 0));
                return RedirectToAction("Index");
            }

            return View(veiculo);
        }

        // GET: Veiculos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Veiculo veiculo = await RedisCacheClient.GetAsync<Veiculo>("Veiculo:" + id) ??
                                     await db.Veiculos.FindAsync(id);

            if (veiculo == null)
            {
                return HttpNotFound();
            }
            return View(veiculo);
        }

        // POST: Veiculos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "VeiculoId,Carro,Marca,Ano,Placa")] Veiculo veiculo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(veiculo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                await RedisCacheClient.ReplaceAsync("Veiculo:" + veiculo.VeiculoId, veiculo, new TimeSpan(0, 10, 0));
                return RedirectToAction("Index");
            }
            return View(veiculo);
        }

        // GET: Veiculos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Veiculo veiculo = await RedisCacheClient.GetAsync<Veiculo>("Veiculo:" + id) ??
                                      await db.Veiculos.FindAsync(id);
            
            if (veiculo == null)
            {
                return HttpNotFound();
            }
            return View(veiculo);
        }

        // POST: Veiculos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Veiculo veiculo = await db.Veiculos.FindAsync(id);
            db.Veiculos.Remove(veiculo);
            await db.SaveChangesAsync();
            await RedisCacheClient.RemoveAsync("Veiculo:" + id);
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

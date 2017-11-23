using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using CodingCraftHOMod1Ex1EF.Models;
using System.Transactions;
using System.Linq;

namespace CodingCraftHOMod1Ex1EF.Controllers
{
    public class EstoquesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public async Task<ActionResult> Index(string consulta = null)
        {
            var estoques = db.Estoques.Include(e => e.Loja).Include(e => e.Produto);

            if (!string.IsNullOrWhiteSpace(consulta))
                estoques = estoques.Where(x => x.Produto.Nome.Contains(consulta));

            return View(await estoques.ToListAsync());
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estoque estoque = await db.Estoques.FindAsync(id);
            if (estoque == null)
            {
                return HttpNotFound();
            }
            return View(estoque);
        }

        public ActionResult Create()
        {
            ViewBag.LojaId = new SelectList(db.Lojas, "LojaId", "Nome");
            ViewBag.ProdutoId = new SelectList(db.Produtos, "ProdutoId", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "EstoqueId,ProdutoId,LojaId,Quantidade")] Estoque estoque)
        {
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    db.Estoques.Add(estoque);
                    await db.SaveChangesAsync();

                    scope.Complete();
                }
                return RedirectToAction("Index");
            }

            ViewBag.LojaId = new SelectList(db.Lojas, "LojaId", "Nome", estoque.LojaId);
            ViewBag.ProdutoId = new SelectList(db.Produtos, "ProdutoId", "Nome", estoque.ProdutoId);
            return View(estoque);
        }

        // GET: Estoques/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estoque estoque = await db.Estoques.FindAsync(id);
            if (estoque == null)
            {
                return HttpNotFound();
            }
            ViewBag.LojaId = new SelectList(db.Lojas, "LojaId", "Nome", estoque.LojaId);
            ViewBag.ProdutoId = new SelectList(db.Produtos, "ProdutoId", "Nome", estoque.ProdutoId);
            return View(estoque);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "EstoqueId,ProdutoId,LojaId,Quantidade")] Estoque estoque)
        {
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    db.Entry(estoque).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    scope.Complete();
                }
                return RedirectToAction("Index");
            }
            ViewBag.LojaId = new SelectList(db.Lojas, "LojaId", "Nome", estoque.LojaId);
            ViewBag.ProdutoId = new SelectList(db.Produtos, "ProdutoId", "Nome", estoque.ProdutoId);
            return View(estoque);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estoque estoque = await db.Estoques.FindAsync(id);
            if (estoque == null)
            {
                return HttpNotFound();
            }
            return View(estoque);
        }

        // POST: Estoques/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                Estoque estoque = await db.Estoques.FindAsync(id);
                db.Estoques.Remove(estoque);
                await db.SaveChangesAsync();

                scope.Complete();
            }
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

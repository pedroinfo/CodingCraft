using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using CodingCraftHOMod1Ex1EF.Models;
using System.Transactions;
using System.Linq;

namespace CodingCraftHOMod1Ex1EF.Controllers
{
    public class LojasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public async Task<ActionResult> Index(string consulta = null)
        {
            var query = db.Lojas.Include(x => x.LojaEstoques);

                if (!string.IsNullOrWhiteSpace(consulta))
                    query = query.Where(x => x.Nome.Contains(consulta));

            return View(await query.ToListAsync());
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loja loja = await db.Lojas.FindAsync(id);
            if (loja == null)
            {
                return HttpNotFound();
            }
            return View(loja);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "LojaId,Nome")] Loja loja)
        {
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    db.Lojas.Add(loja);
                    await db.SaveChangesAsync();
                    scope.Complete();
                }
                return RedirectToAction("Index");
            }
            return View(loja);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loja loja = await db.Lojas.FindAsync(id);
            if (loja == null)
            {
                return HttpNotFound();
            }
            return View(loja);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "LojaId,Nome")] Loja loja)
        {
            if (ModelState.IsValid)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    db.Entry(loja).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    scope.Complete();
                }
            }
            return View(loja);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loja loja = await db.Lojas.FindAsync(id);
            if (loja == null)
            {
                return HttpNotFound();
            }
            return View(loja);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                Loja loja = await db.Lojas.FindAsync(id);
                db.Lojas.Remove(loja);
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

using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using CodingCraftHOMod1Ex3Modularizacao.Dominio.Models;

namespace CodingCraftHOMod1Ex3Modularizacao.Mvc.Comum.Controllers
{
    [Authorize(Roles = "Aprovação")]
    public class AprovacaoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Revisao
        public async Task<ActionResult> Index()
        {
            var textos = db.Textos.Include(t => t.Curso).Include(t => t.ApplicationUser);
            return View(await textos.ToListAsync());
        }

        // GET: Revisao/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Texto texto = await db.Textos.FindAsync(id);
            if (texto == null)
            {
                return HttpNotFound();
            }
            return View(texto);
        }

        // GET: Revisao/Create
        public ActionResult Create()
        {
            ViewBag.CursoId = new SelectList(db.Cursos, "CursoId", "Nome");
        //    ViewBag.FuncionarioId = new SelectList(db.Funcionarios, "FuncionarioId", "Nome");
            return View();
        }

        // POST: Revisao/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TextoId,CursoId,FuncionarioId,Titulo,Conteudo,DataHoraTexto")] Texto texto)
        {
            if (ModelState.IsValid)
            {
                db.Textos.Add(texto);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CursoId = new SelectList(db.Cursos, "CursoId", "Nome", texto.CursoId);
          //  ViewBag.FuncionarioId = new SelectList(db.Funcionarios, "FuncionarioId", "Nome", texto.UserId);
            return View(texto);
        }

        // GET: Revisao/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Texto texto = await db.Textos.FindAsync(id);
            if (texto == null)
            {
                return HttpNotFound();
            }
            ViewBag.CursoId = new SelectList(db.Cursos, "CursoId", "Nome", texto.CursoId);
          //  ViewBag.FuncionarioId = new SelectList(db.Funcionarios, "FuncionarioId", "Nome", texto.UserId);
            return View(texto);
        }

        // POST: Revisao/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TextoId,CursoId,FuncionarioId,Titulo,Conteudo")] Texto texto)
        {
            if (ModelState.IsValid)
            {
                db.Textos.Attach(texto);
                db.Entry(texto).Property(x => x.CursoId).IsModified = true;
                db.Entry(texto).Property(x => x.Conteudo).IsModified = true;
                db.Entry(texto).Property(x => x.Titulo).IsModified = true;

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CursoId = new SelectList(db.Cursos, "CursoId", "Nome", texto.CursoId);
            // ViewBag.FuncionarioId = new SelectList(db.Funcionarios, "FuncionarioId", "Nome", texto.UserId);
            return View(texto);
        }

        [HttpPost]
        public async Task<ActionResult> ConcluirAprovacao(int id)
        {
            var texto = new Texto
            {
                TextoId = id,
                Aprovado = true
            };

            if (ModelState.IsValid)
            {
                db.Textos.Attach(texto);
                db.Entry(texto).Property(x => x.Aprovado).IsModified = true;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Revisao/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Texto texto = await db.Textos.FindAsync(id);
            if (texto == null)
            {
                return HttpNotFound();
            }
            return View(texto);
        }

        // POST: Revisao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Texto texto = await db.Textos.FindAsync(id);
            db.Textos.Remove(texto);
            await db.SaveChangesAsync();
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

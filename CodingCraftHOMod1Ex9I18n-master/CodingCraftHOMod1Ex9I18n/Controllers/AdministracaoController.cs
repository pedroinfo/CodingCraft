using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using CodingCraftHOMod1Ex9I18n.Models;
using System.Web;

namespace CodingCraftHOMod1Ex9I18n.Controllers
{
    public class AdministracaoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
         
        // GET: Livros
        public async Task<ActionResult> Index()
        {
            return View(await db.Livros.ToListAsync());
        }

        // GET: Livros/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livro livro = await db.Livros.FindAsync(id);
            if (livro == null)
            {
                return HttpNotFound();
            }
            return View(livro);
        }

        // GET: Livros/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Livros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "LivroId,Titulo,Editora,Assunto,Descricao,Preco")] Livro livro, HttpPostedFileBase Imagem)
        {
            if (ModelState.IsValid)
            {
                if (Imagem != null)
                {
                    using (var reader = new System.IO.BinaryReader(Imagem.InputStream))
                    {
                        livro.Imagem = reader.ReadBytes(Imagem.ContentLength);
                    }
                }

                db.Livros.Add(livro);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(livro);
        }

        // GET: Livros/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livro livro = await db.Livros.FindAsync(id);
            if (livro == null)
            {
                return HttpNotFound();
            }
            return View(livro);
        }

        // POST: Livros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "LivroId,Titulo,Editora,Assunto,Descricao,Preco")] Livro livro, HttpPostedFileBase Imagem)
        {
            if (ModelState.IsValid)
            {
                if (Imagem != null)
                {
                    using (var reader = new System.IO.BinaryReader(Imagem.InputStream))
                    {
                        livro.Imagem = reader.ReadBytes(Imagem.ContentLength);
                    }
                }

                db.Entry(livro).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(livro);
        }

        // GET: Livros/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livro livro = await db.Livros.FindAsync(id);
            if (livro == null)
            {
                return HttpNotFound();
            }
            return View(livro);
        }

        // POST: Livros/Delete/5
        [HttpPost, ActionName("Delete")]
      //  [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Livro livro = await db.Livros.FindAsync(id);
            db.Livros.Remove(livro);
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

using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using CodingCraftHOMod1Ex1EF.Models;



namespace CodingCraftHOMod1Ex1EF.Controllers
{
    public class RelatoriosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public async Task <ActionResult> Index()
        {
            var vendas = db.Vendas.Include(c => c.Cliente).Include(p => p.Produto);
            return View(await vendas.ToListAsync());
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
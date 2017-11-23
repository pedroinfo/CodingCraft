using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using CodingCraftHOMod1Ex6Dapper.Models;
using Dapper;
using System.Collections.Generic;
using StackExchange.Profiling;
using System.Linq;

namespace CodingCraftHOMod1Ex6Dapper.Controllers
{
    public class AgricultureGdpsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AgricultureGdps
        public async Task<ActionResult> Index()
        {
            // var agricultureGdps = db.AgricultureGdps.Include(a => a.Country);
            var profiler = MiniProfiler.Current;

            IEnumerable<AgricultureGdp> agricultureGdps;
            using (profiler.Step("Query do Dapper"))
            {
                var sql = @"select a.AgricultureGdpId, a.CountryId, a.Year, a.Value, c.Name, c.CountryId
                            from AgricultureGdps a
                            inner join Countries c on a.CountryId = c.CountryId";

                var lookup = new Dictionary<int, AgricultureGdp>();

                agricultureGdps = await db.Database.Connection.QueryAsync<AgricultureGdp, Country, AgricultureGdp>(
                    sql, (agricultureGdp, country) => {
                        AgricultureGdp result;
                        if (!lookup.TryGetValue(agricultureGdp.AgricultureGdpId, out result))
                        {
                            lookup.Add(agricultureGdp.AgricultureGdpId, result = agricultureGdp);
                        }

                        result.Country = country;

                        return result;
                    }, splitOn: "Value,Name"
                );

                return View(lookup.Values.ToList());
            }

            // return View(agricultureGdps);
        }

        // GET: AgricultureGdps/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgricultureGdp agricultureGdp = await db.AgricultureGdps.FindAsync(id);
            if (agricultureGdp == null)
            {
                return HttpNotFound();
            }
            return View(agricultureGdp);
        }

        // GET: AgricultureGdps/Create
        public ActionResult Create()
        {
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "Name");
            return View();
        }

        // POST: AgricultureGdps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AgricultureGdpId,CountryId,Year,Value")] AgricultureGdp agricultureGdp)
        {
            if (ModelState.IsValid)
            {
                db.AgricultureGdps.Add(agricultureGdp);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "Name", agricultureGdp.CountryId);
            return View(agricultureGdp);
        }

        // GET: AgricultureGdps/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgricultureGdp agricultureGdp = await db.AgricultureGdps.FindAsync(id);
            if (agricultureGdp == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "Name", agricultureGdp.CountryId);
            return View(agricultureGdp);
        }

        // POST: AgricultureGdps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AgricultureGdpId,CountryId,Year,Value")] AgricultureGdp agricultureGdp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agricultureGdp).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "Name", agricultureGdp.CountryId);
            return View(agricultureGdp);
        }

        // GET: AgricultureGdps/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgricultureGdp agricultureGdp = await db.AgricultureGdps.FindAsync(id);
            if (agricultureGdp == null)
            {
                return HttpNotFound();
            }
            return View(agricultureGdp);
        }

        // POST: AgricultureGdps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            AgricultureGdp agricultureGdp = await db.AgricultureGdps.FindAsync(id);
            db.AgricultureGdps.Remove(agricultureGdp);
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

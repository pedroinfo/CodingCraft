using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CodingCraftHOMod1Ex5WebAPI.Models;
using CodingCraftHOMod1Ex5WebAPI.Models.Contexto;
using System.Web;

namespace CodingCraftHOMod1Ex5WebAPI.Controllers
{
    public class ArquivoTipoController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ArquivoTipo
        public IQueryable<ArquivoTipo> GetArquivosTipos()
        {
            return db.ArquivosTipos;
        }

        // GET: api/ArquivoTipo/5
        [ResponseType(typeof(ArquivoTipo))]
        public async Task<IHttpActionResult> GetArquivoTipo(int id)
        {
            ArquivoTipo arquivoTipo = await db.ArquivosTipos.FindAsync(id);
            if (arquivoTipo == null)
            {
                return NotFound();
            }

            return Ok(arquivoTipo);
        }

        // PUT: api/ArquivoTipo/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutArquivoTipo(int id, ArquivoTipo arquivoTipo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != arquivoTipo.ArquivoTipoId)
            {
                return BadRequest();
            }

            db.Entry(arquivoTipo).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArquivoTipoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ArquivoTipo
        [ResponseType(typeof(ArquivoTipo))]
        public async Task<IHttpActionResult> PostArquivoTipo(ArquivoTipo arquivoTipo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ArquivosTipos.Add(arquivoTipo);
            await db.SaveChangesAsync();

            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                var docfiles = new List<string>();
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var filePath = HttpContext.Current.Server.MapPath("~/Uploads/" + postedFile.FileName);
                    postedFile.SaveAs(filePath);

                    docfiles.Add(filePath);
                }
                result = Request.CreateResponse(HttpStatusCode.Created, docfiles);
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }


            return CreatedAtRoute("DefaultApi", new { id = arquivoTipo.ArquivoTipoId }, arquivoTipo);
        }

        // DELETE: api/ArquivoTipo/5
        [ResponseType(typeof(ArquivoTipo))]
        public async Task<IHttpActionResult> DeleteArquivoTipo(int id)
        {
            ArquivoTipo arquivoTipo = await db.ArquivosTipos.FindAsync(id);
            if (arquivoTipo == null)
            {
                return NotFound();
            }

            db.ArquivosTipos.Remove(arquivoTipo);
            await db.SaveChangesAsync();

            return Ok(arquivoTipo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ArquivoTipoExists(int id)
        {
            return db.ArquivosTipos.Count(e => e.ArquivoTipoId == id) > 0;
        }

        
        
    }
}
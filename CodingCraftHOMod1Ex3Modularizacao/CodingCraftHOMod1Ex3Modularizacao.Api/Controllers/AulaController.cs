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
using CodingCraftHOMod1Ex3Modularizacao.Dominio.Models;

namespace CodingCraftHOMod1Ex3Modularizacao.Api.Controllers
{
    [Authorize(Roles = "Aluno")]
    public class AulaController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Aula
        public IQueryable<Texto> GetTextos()
        {
            return db.Textos;
        }

        // GET: api/Aula/5
        [ResponseType(typeof(Texto))]
        public async Task<IHttpActionResult> GetTexto(int id)
        {
            Texto texto = await db.Textos.FindAsync(id);
            if (texto == null)
            {
                return NotFound();
            }

            return Ok(texto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TextoExists(int id)
        {
            return db.Textos.Count(e => e.TextoId == id) > 0;
        }
    }
}
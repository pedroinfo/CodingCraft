using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CodingCraftHOMod1Ex5WebAPI.Models;
using System.Collections.Generic;
using CodingCraftHOMod1Ex5WebAPI.Models.Identity;
using CodingCraftHOMod1Ex5WebAPI.Models.Contexto;

namespace CodingCraftHOMod1Ex5WebAPI.Controllers
{
    [Authorize]
    public class ProdutosController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Produtos
        public IEnumerable<dynamic> GetProdutos()
        {
            return db.Produtos
                .Include(p => p.Categoria)
                .Select(x => new {
                x.ProdutoId,
                x.Descricao,
                x.Preco,
                x.Categoria
            }).ToList();
        }

        // GET: api/Produtos/5
        [ResponseType(typeof(Produto))]
        public async Task<IHttpActionResult> GetProduto(Guid id)
        {
            Produto produto = await db.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }

            return Ok(produto);
        }

        // PUT: api/Produtos/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProduto(Guid id, Produto produto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != produto.ProdutoId)
            {
                return BadRequest();
            }

            db.Entry(produto).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoExists(id))
                {
                    return NotFound();
                }

                throw;

            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Produtos
        [ResponseType(typeof(Produto))]
        public async Task<IHttpActionResult> PostProduto(Produto produto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            produto.ProdutoId = Guid.NewGuid();
            db.Produtos.Add(produto);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProdutoExists(produto.ProdutoId))
                {
                    return Conflict();
                }

                throw;
            }

            return CreatedAtRoute("DefaultApi", new { id = produto.ProdutoId }, produto);
        }

        // DELETE: api/Produtos/5
        [ResponseType(typeof(Produto))]
        public async Task<IHttpActionResult> DeleteProduto(Guid id)
        {
            Produto produto = await db.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }

            db.Produtos.Remove(produto);
            await db.SaveChangesAsync();

            return Ok(produto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProdutoExists(Guid id)
        {
            return db.Produtos.Count(e => e.ProdutoId == id) > 0;
        }
    }
}
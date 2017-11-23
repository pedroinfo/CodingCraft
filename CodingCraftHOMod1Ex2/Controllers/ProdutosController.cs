using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EX2.Models;

namespace EX2.Controllers
{   
    public class ProdutosController : Controller
    {
        private EX2Context context = new EX2Context();

        //
        // GET: /Produtoes/

        public ViewResult Indice()
        {
            return View(context.Produtoes.ToList());
        }

        //
        // GET: /Produtoes/Details/5

        public ViewResult Detalhes(int id)
        {
            Produto produto = context.Produtoes.Single(x => x.ProdutoId == id);
            return View(produto);
        }

        //
        // GET: /Produtoes/Create

        public ActionResult Criar()
        {
            return View();
        } 

        //
        // POST: /Produtoes/Create

        [HttpPost]
        public ActionResult Criar(Produto produto)
        {
            if (ModelState.IsValid)
            {
                context.Produtoes.Add(produto);
                context.SaveChanges();
                return RedirectToAction("Indice");  
            }

            return View(produto);
        }
        
        //
        // GET: /Produtoes/Edit/5
 
        public ActionResult Editar(int id)
        {
            Produto produto = context.Produtoes.Single(x => x.ProdutoId == id);
            return View(produto);
        }

        //
        // POST: /Produtoes/Edit/5

        [HttpPost]
        public ActionResult Editar(Produto produto)
        {
            if (ModelState.IsValid)
            {
                context.Entry(produto).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Indice");
            }
            return View(produto);
        }

        //
        // GET: /Produtoes/Delete/5
 
        public ActionResult Excluir(int id)
        {
            Produto produto = context.Produtoes.Single(x => x.ProdutoId == id);
            return View(produto);
        }

        //
        // POST: /Produtoes/Delete/5

        [HttpPost, ActionName("Excluir")]
        public ActionResult ExcluirConfirmacao(int id)
        {
            Produto produto = context.Produtoes.Single(x => x.ProdutoId == id);
            context.Produtoes.Remove(produto);
            context.SaveChanges();
            return RedirectToAction("Indice");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
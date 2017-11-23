using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EX2.Models;
using EX2.Filter;

namespace EX2.Controllers
{
    [LayoutChooser(LayoutName = "_LayoutFoundation")]
    public class ClientesController : Controller
    {
        private EX2Context context = new EX2Context();

        //
        // GET: /Clientes/

        public ViewResult Indice()
        {
            return View(context.Clientes.ToList());
        }

        //
        // GET: /Clientes/Details/5

        public ViewResult Detalhes(int id)
        {
            Cliente cliente = context.Clientes.Single(x => x.ClienteId == id);
            return View(cliente);
        }

        //
        // GET: /Clientes/Create

        public ActionResult Criar()
        {
            return View();
        } 

        //
        // POST: /Clientes/Create

        [HttpPost]
        public ActionResult Criar(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                context.Clientes.Add(cliente);
                context.SaveChanges();
                return RedirectToAction("Indice");  
            }

            return View(cliente);
        }
        
        //
        // GET: /Clientes/Edit/5
 
        public ActionResult Editar(int id)
        {
            Cliente cliente = context.Clientes.Single(x => x.ClienteId == id);
            return View(cliente);
        }

        //
        // POST: /Clientes/Edit/5

        [HttpPost]
        public ActionResult Editar(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                context.Entry(cliente).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Indice");
            }
            return View(cliente);
        }

        //
        // GET: /Clientes/Delete/5
 
        public ActionResult Excluir(int id)
        {
            Cliente cliente = context.Clientes.Single(x => x.ClienteId == id);
            return View(cliente);
        }

        //
        // POST: /Clientes/Delete/5

        [HttpPost, ActionName("Excluir")]
        public ActionResult ExcluirConfirmacao(int id)
        {
            Cliente cliente = context.Clientes.Single(x => x.ClienteId == id);
            context.Clientes.Remove(cliente);
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
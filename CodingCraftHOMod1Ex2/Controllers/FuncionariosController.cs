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
    [LayoutChooser(LayoutName = "_Layout_Ink")]
    public class FuncionariosController : Controller
    {
        private EX2Context context = new EX2Context();

        //
        // GET: /Funcionarios/

        public ViewResult Indice()
        {
            return View(context.Funcionarios.ToList());
        }

        //
        // GET: /Funcionarios/Details/5

        public ViewResult Detalhes(int id)
        {
            Funcionario funcionario = context.Funcionarios.Single(x => x.FuncionarioId == id);
            return View(funcionario);
        }

        //
        // GET: /Funcionarios/Create

        public ActionResult Criar()
        {
            return View();
        } 

        //
        // POST: /Funcionarios/Create

        [HttpPost]
        public ActionResult Criar(Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                context.Funcionarios.Add(funcionario);
                context.SaveChanges();
                return RedirectToAction("Indice");  
            }

            return View(funcionario);
        }
        
        //
        // GET: /Funcionarios/Edit/5
 
        public ActionResult Editar(int id)
        {
            Funcionario funcionario = context.Funcionarios.Single(x => x.FuncionarioId == id);
            return View(funcionario);
        }

        //
        // POST: /Funcionarios/Edit/5

        [HttpPost]
        public ActionResult Editar(Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                context.Entry(funcionario).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Indice");
            }
            return View(funcionario);
        }

        //
        // GET: /Funcionarios/Delete/5
 
        public ActionResult Excluir(int id)
        {
            Funcionario funcionario = context.Funcionarios.Single(x => x.FuncionarioId == id);
            return View(funcionario);
        }

        //
        // POST: /Funcionarios/Delete/5

        [HttpPost, ActionName("Excluir")]
        public ActionResult ExcluirConfirmacao(int id)
        {
            Funcionario funcionario = context.Funcionarios.Single(x => x.FuncionarioId == id);
            context.Funcionarios.Remove(funcionario);
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
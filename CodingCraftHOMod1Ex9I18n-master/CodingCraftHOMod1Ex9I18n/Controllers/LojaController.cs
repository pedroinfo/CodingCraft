using CodingCraftHOMod1Ex9I18n.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodingCraftHOMod1Ex9I18n.Controllers
{
    public class LojaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var livros = db.Livros.ToList();

            return View(livros);
        }
    }
}
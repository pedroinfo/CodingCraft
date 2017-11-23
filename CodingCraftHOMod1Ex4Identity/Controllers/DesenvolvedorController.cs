using CodingCraftHOMod1Ex4Identity.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace CodingCraftHOMod1Ex4Identity.Controllers
{
    public class DesenvolvedorController : Controller
    {
        // GET: Desenvolvedor
        public ActionResult Index(string claim)
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;

            if (claims.Any(x => x.Type.Equals(ClaimsSistema.Desenvolvedor.ToString()) && x.Value.Equals(claim)))
                ViewBag.AcessoAutorizado = true;
            else
                ViewBag.AcessoAutorizado = false;

            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodingCraftHOMod1Ex9I18n.Controllers
{

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string incomingUrl = HttpContext.Request.Url.LocalPath;

            if (incomingUrl == "/")
                return Redirect("/en-us/home");

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
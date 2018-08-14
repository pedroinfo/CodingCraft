using CodingCraftHOMod1Ex9I18n.Helpers;
using System;
using System.Threading;

namespace CodingCraftHOMod1Ex9I18n.Controllers
{
    public abstract class Controller : System.Web.Mvc.Controller
    {
        /* protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string cultureName = RouteData.Values["culture"] as string;

            if (cultureName == null)
                cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ? Request.UserLanguages[0] : null; // obtain it from HTTP header AcceptLanguages

            cultureName = CultureHelper.GetImplementedCulture(cultureName); // Veja mais abaixo na resposta

            if (RouteData.Values["culture"] as string != cultureName)
            {
                // Força uma cultura válida na URL
                RouteData.Values["culture"] = cultureName.ToLowerInvariant();
                Response.RedirectToRoute(RouteData.Values);
            }

            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            return base.BeginExecuteCore(callback, state);
        } */

    }
}
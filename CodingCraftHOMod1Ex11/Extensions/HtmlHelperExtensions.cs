using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace EX11.Extensions
{
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Gera uma tag <a> com os parâmetros de pesquisa (query string) atuais.
        /// </summary>
        /// <param name="htmlHelper">O HtmlHelper.</param>
        /// <param name="linkText">O texto do link.</param>
        /// <param name="action">A Action.</param>
        /// <returns>A tag <a> estruturada.</returns>
        public static MvcHtmlString ActionQueryLink(this HtmlHelper htmlHelper,
            string linkText, string action, object htmlAttributes = null)
        {
            return ActionQueryLink(htmlHelper, linkText, action, null, htmlAttributes);
        }

        /// <summary>
        /// Gera uma tag <a> com os parâmetros de pesquisa (query string) atuais.
        /// </summary>
        /// <param name="htmlHelper">O HtmlHelper.</param>
        /// <param name="linkText">O texto do link.</param>
        /// <param name="action">A Action.</param>
        /// <param name="routeValues">Valores adicionais da rota.</param>
        /// <returns>A tag <a> estruturada.</returns>
        public static MvcHtmlString ActionQueryLink(this HtmlHelper htmlHelper,
            string linkText, string action, object routeValues = null, object htmlAttributes = null)
        {
            var queryString =
                htmlHelper.ViewContext.HttpContext.Request.QueryString;

            var newRoute = routeValues == null
                ? htmlHelper.ViewContext.RouteData.Values
                : new RouteValueDictionary(routeValues);

            foreach (string key in queryString.Keys)
            {
                if (!newRoute.ContainsKey(key))
                    newRoute.Add(key, queryString[key]);
            }

            return new MvcHtmlString(HtmlHelper.GenerateLink(htmlHelper.ViewContext.RequestContext,
                htmlHelper.RouteCollection, linkText, null /* routeName */,
                action, null, newRoute, htmlAttributes.GetType().GetProperties().ToDictionary(x => x.Name, x => x.GetValue(htmlAttributes, null))));
        }

        /// <summary>
        /// Gera uma tag <a> para voltar para a tela anterior, mas com os parâmetros de pesquisa (query string) preenchidos.
        /// </summary>
        /// <param name="htmlHelper">O HtmlHelper.</param>
        /// <param name="linkText">O texto do link.</param>
        /// <param name="action">A Action.</param>
        /// <param name="controller">O Controller.</param>
        /// <param name="routeValues">Valores adicionais da rota.</param>
        /// <returns>A tag <a> estruturada.</returns>
        public static MvcHtmlString ActionReferrerQuery(this HtmlHelper htmlHelper,
            string linkText, string action, string controller, object routeValues)
        {
            var referrer = htmlHelper.ViewContext.HttpContext.Request.UrlReferrer;

            if (referrer == null || referrer.Query == null) return new MvcHtmlString(HtmlHelper.GenerateLink(htmlHelper.ViewContext.RequestContext,
                htmlHelper.RouteCollection, linkText, "Default", action, controller, null, null));

            var queryString = referrer.Query.Replace("?", "");

            var newRoute = new RouteValueDictionary(routeValues);

            if (!string.IsNullOrEmpty(queryString))
            {
                foreach (string key in queryString.Split('&'))
                {
                    var keyValuePair = key.Split('=');
                    if (!newRoute.ContainsKey(keyValuePair[0]))
                        newRoute.Add(keyValuePair[0], keyValuePair[1]);
                }
            }

            return new MvcHtmlString(HtmlHelper.GenerateLink(htmlHelper.ViewContext.RequestContext,
                htmlHelper.RouteCollection, linkText, "Default", action, controller, newRoute, null));
        }
    }

}
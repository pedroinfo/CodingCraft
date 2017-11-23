using System.Web.Mvc;
using System.Web.Routing;

namespace EX11.Extensions
{
    public static class UrlHelperExtensions
    {
        /// <summary>
        /// Gera um link com os parâmetros de pesquisa (query string) atuais.
        /// </summary>
        /// <param name="urlHelper">O UrlHelper.</param>
        /// <param name="linkText">O texto do link.</param>
        /// <param name="action">A Action.</param>
        /// <param name="controller">O Controller.</param>
        /// <returns>O link estruturado.</returns>
        public static string ActionQuery(this UrlHelper urlHelper,
            string action, string controller)
        {
            return ActionQuery(urlHelper, action, controller, null);
        }

        /// <summary>
        /// Gera um link com os parâmetros de pesquisa (query string) atuais.
        /// </summary>
        /// <param name="urlHelper">O UrlHelper.</param>
        /// <param name="action">A Action.</param>
        /// <param name="controller">O Controller.</param>
        /// <param name="routeValues">Valores adicionais da rota.</param>
        /// <returns>O link estruturado.</returns>
        public static string ActionQuery(this UrlHelper urlHelper,
            string action, string controller, object routeValues)
        {
            var queryString =
                urlHelper.RequestContext.HttpContext.Request.QueryString;

            var newRoute = routeValues == null
                ? urlHelper.RequestContext.RouteData.Values
                : new RouteValueDictionary(routeValues);

            foreach (string key in queryString.Keys)
            {
                if (!newRoute.ContainsKey(key))
                    newRoute.Add(key, queryString[key]);
            }

            return UrlHelper.GenerateUrl("Default", action, controller, newRoute,
                urlHelper.RouteCollection, urlHelper.RequestContext, true);
        }

        /// <summary>
        /// Gera um link para voltar para a tela anterior, mas com os parâmetros de pesquisa (query string) preenchidos.
        /// </summary>
        /// <param name="urlHelper">O UrlHelper.</param>
        /// <param name="action">A Action.</param>
        /// <param name="controller">O Controller.</param>
        /// <param name="routeValues">Valores adicionais da rota.</param>
        /// <returns>O link estruturado.</returns>
        public static string ActionReferrerQuery(this UrlHelper urlHelper,
            string action, string controller, object routeValues)
        {
            var referrer = urlHelper.RequestContext.HttpContext.Request.UrlReferrer;
            if (referrer.Query == null) return UrlHelper.GenerateUrl("Default", action, controller, null,
                urlHelper.RouteCollection, urlHelper.RequestContext, true);

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

            return UrlHelper.GenerateUrl("Default", action, controller, newRoute,
                urlHelper.RouteCollection, urlHelper.RequestContext, true);
        }
    }
}
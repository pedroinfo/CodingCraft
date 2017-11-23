using System.Web;
using System.Web.Mvc;

namespace CodingCraftHOMod1Ex3Modularizacao.Api
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}

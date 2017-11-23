using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EX2.Filter
{
    public class LayoutChooserAttribute : ActionFilterAttribute
    {
        public string LayoutName { get; set; } = "_Layout";
        
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string masterName = LayoutName;

            base.OnActionExecuted(filterContext);
            string userName = null;
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                userName = filterContext.HttpContext.User.Identity.Name;
            }

            var result = filterContext.Result as ViewResult;
            if (result != null)
            {
                result.MasterName = masterName;
            }
        }
    }
}
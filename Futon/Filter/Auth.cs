using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Futon.Filter
{
    public class Auth:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["loginedAdmin"] == null)
            {
                filterContext.Result = new RedirectResult("~/manage/home/index");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
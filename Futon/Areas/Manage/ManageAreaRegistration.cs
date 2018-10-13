using System.Web.Mvc;

namespace Futon.Areas.Manage
{
    public class ManageAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "manage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {


          

            context.MapRoute(
            "Manage_default",
            "manage/{controller}/{action}/{id}",

             namespaces: new[] { "Futon.Areas.Manage.Controllers" },
            defaults: new { controller = "home", action = "login", id = UrlParameter.Optional }

        );

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Futon.Controllers
{
    public class DepartmentController : Controller
    {
        // GET: Department
        public ActionResult Index(int id)
        {
            return Content("test");
        }
    }
}
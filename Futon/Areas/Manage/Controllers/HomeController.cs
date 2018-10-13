using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Futon.Models;
using System.Web.Helpers;

namespace Futon.Areas.Manage.Controllers
{
    public class HomeController : Controller
    {
        FutonEntities1 db = new FutonEntities1();
        // GET: Manage/Home

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Admin adm)
        {

            var login = db.Admins.FirstOrDefault(a => a.Email == adm.Email);

            if (login != null)
            {
                if (Crypto.VerifyHashedPassword(login.Password, adm.Password))
                {
                    Session["loginedAdmin"] = true;
                    Session["Admin"] = login;
                    return RedirectToAction("index", "products");
                }

            }
            Session["loginError"] = "istifadeci adi ve ya sifre yanlisdir";
            return RedirectToAction("index");

        }
        public ActionResult Logout()
        {
            Session["loginedAdmin"] = null;
            Session["Admin"] = null;
            return RedirectToAction("index","home");
        }
    }
}
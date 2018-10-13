using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using Futon.Models;
using System.Net.Mail;
using System.Net;
using System.Data.Entity.Migrations;

namespace Futon.Controllers
{
    public class AccountController : Controller
    {
        FutonEntities1 db = new FutonEntities1();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Auth()
        {
            return View();
        }

        public ActionResult Register(User usr)
        {
            if(usr.Username==null || usr.Email==null || usr.Password == null)
            {
                Session["loginerror"] = "Username , email and password are required";
                return RedirectToAction("auth");
            }

            if (db.Users.FirstOrDefault(u => u.Email == usr.Email)!=null)
            {
                Session["loginerror"] = "This email was registrated,please change your password";
                return RedirectToAction("auth");
            }
            usr.Token = Crypto.Hash(usr.Email + DateTime.Now.ToString("yyyyMMddHHmmss"), "sha256");
            usr.Password = Crypto.HashPassword(usr.Password);

            db.Users.Add(usr);
          
            db.SaveChanges();
            SendConfirmEmail(usr.Email, usr.Token);
            Session["success"] = "Emailden Uzvluyunuzu tesdiq edin";
            return RedirectToAction("auth");

        }
        private bool SendConfirmEmail(string email, string token)
        {
            var body = "<p>Futon.az Xos geldiniz: üzvlüyünüzü təsdiqləmək üçün <a href='"+ string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~")) + "/account/confirm?token=" + token + "'>tıklayın</a></p>";
            var message = new MailMessage();
            message.To.Add(new MailAddress(email));  // replace with valid value 
            message.From = new MailAddress("no-reply@futon.az");  // replace with valid value
            message.Subject = "Üzvlüyünüzü təsdiq";
            message.Body = body;
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "futonshopaz@gmail.com",  // replace with valid value
                    Password = "futon123321"  // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Send(message);
                return true;
            }
        }

        public ActionResult Confirm(string token)
        {
            User usr = db.Users.FirstOrDefault(u => u.Token == token);
            if (usr == null)
            {
                return HttpNotFound();
            }

            if (usr.IsAccepted)
            {
                return HttpNotFound();
            }

            usr.IsAccepted = true;
            db.SaveChanges();
            Session["UserConfirmed"] = true;
            return RedirectToAction("index", "home");
        }

        public JsonResult Checkmail(string email, int type = 1)
        {
            if (type == 1)
            {
                if (db.Users.FirstOrDefault(u => u.Email == email) == null)
                {
                    return Json(new
                    {
                        valid = true

                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        valid = false,
                        message = "Bu e-poçtla qeydiyyatdan keçilib, şifrənizi yeniləyə bilərsiniz"
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                if (db.Users.FirstOrDefault(u => u.Email == email) == null)
                {
                    return Json(new
                    {
                        valid = false,
                        message = "Bu e-poçtla qeydiyyatdan keçilməyib"
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        valid = true,
                    }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpPost]
        public ActionResult Login(User user)
        {

            if (user.Email == null || user.Password == null)
            {
                Session["RegisterError"] = "Ad soyad,E-poçt və şifrə boş ola bilməz";
                return RedirectToAction("Auth");
            }
            User usr = db.Users.FirstOrDefault(u => u.Email == user.Email);
            if (usr != null)
            {
                if (usr.IsAccepted)
                {
                    if (Crypto.VerifyHashedPassword(usr.Password, user.Password))
                    {

                        Session["Logined"] = true;
                        Session["User"] = usr;
                        return RedirectToAction("index", "home");
                    }
                    else
                    {
                        Session["LoginError"] = "E-poçt vəya şifrə səhvdir";

                    }
                }
                else
                {
                    Session["LoginError"] = "Email tesdiqlenmemisdir";
                }

            }
            else
            {
                Session["LoginError"] = "E-poçt vəya şifrə səhvdir";
            }
           
            return RedirectToAction("Auth");
        }

        public ActionResult LogOut()
        {
            Session["Logined"] = null;
            Session["User"] = null;
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public ActionResult Forget()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Forget(string email)
        {
            User usr = db.Users.FirstOrDefault(u => u.Email == email);
            if (usr == null)
            {
                return HttpNotFound();
            }

            usr.ForgetToken = Crypto.Hash(usr.Email + DateTime.Now.ToString("yyyyMMddHHmmss"), "sha256");
            db.SaveChanges();
            SendForgetEmail(usr.Email, usr.ForgetToken);
            Session["message"] = "Sizə şifrə sıfırlma e-poçt göndərildi";
            return RedirectToAction("forget");

        }
        private bool SendForgetEmail(string email ,string ForgetToken)
        {
            var body = "<p>Sifrenizi deyismek ucun <a href='" + string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~")) + "/account/changepassword?forgettoken=" + ForgetToken + "'>tıklayın</a></p>";
            var message = new MailMessage();
            message.To.Add(new MailAddress(email));  // replace with valid value 
            message.From = new MailAddress("no-reply@futon.az");  // replace with valid value
            message.Subject = "Sifre deyistirmek";
            message.Body = body;
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "futonshopaz@gmail.com",  // replace with valid value
                    Password = "futon123321"  // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Send(message);
                return true;
            }
        }

        public ActionResult ChangePassword(string forgetToken)
        {
            User usr = db.Users.FirstOrDefault(u => u.ForgetToken == forgetToken);
            if (usr == null)
            {
                return HttpNotFound();
            }

            return View(model: forgetToken);
        }
        
        [HttpPost]
        public ActionResult ChangePassword(string forgetToken, string password)
        {
            User usr = db.Users.FirstOrDefault(u => u.ForgetToken == forgetToken);
            if (usr == null)
            {
                return HttpNotFound();
            }
            usr.Password = Crypto.HashPassword(password);
            usr.ForgetToken = null;
            db.SaveChanges();
            Session["success"] = "Sizin şifrəniz yeniləndi";
            return RedirectToAction("auth");
        }
    }
}
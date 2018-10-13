using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Futon.Models;
using Futon.Filter;

namespace Futon.Areas.Manage.Controllers
{
    [Auth]
    public class SettingController : Controller
    {
        FutonEntities1 db = new FutonEntities1();
        // GET: Manage/Setting
        public ActionResult Index()
        {
            Setting setting = db.Settings.FirstOrDefault();
            return View(setting);
        }
        [HttpGet]
        public ActionResult Edit()
        {
            Setting setting = db.Settings.FirstOrDefault();
            
            return View(setting);
        }
        [HttpPost]
        public ActionResult Edit(Setting stng, HttpPostedFileBase photo, string deletedphoto)
        {
            Setting set = db.Settings.FirstOrDefault();
            if (ModelState.IsValid)
            {
                if (photo != null &&(photo.ContentType=="image/jpeg" || photo.ContentType == "image/png" ||  photo.ContentType == "image/jpg"))
                {
                    if (deletedphoto != null)
                    {
                        var fileDeletedPath = Path.Combine(Server.MapPath("~/Uploads/Setting"), deletedphoto);

                        System.IO.File.Delete(fileDeletedPath);
                    }
                 
                    
                    var newfilePath = Path.Combine(HttpContext.Server.MapPath("~/Uploads/Setting"), photo.FileName);
                    photo.SaveAs(newfilePath);
                    set.logo = photo.FileName;

                }

                set.Adress = stng.Adress;
                set.Email = stng.Email;
                set.Facebook = stng.Facebook;
                
                set.Phone = stng.Phone;
                set.Twitter = stng.Twitter;
                db.SaveChanges();

            }

            return RedirectToAction("index", "setting");
        }
    }
}
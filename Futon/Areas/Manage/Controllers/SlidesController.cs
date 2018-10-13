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
    public class SlidesController : Controller
    {
        FutonEntities1 db = new FutonEntities1();
        // GET: Manage/Slides
        public ActionResult Index()
        {
            List<Slide> slides = db.Slides.ToList();
            return View(slides);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Slide sld, HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                if(photo != null && (photo.ContentType == "image/jpeg" || photo.ContentType == "image/png" || photo.ContentType == "image/jpg"))
                {
                    var newfilePath = Path.Combine(HttpContext.Server.MapPath("~/Uploads/Slides"), photo.FileName);
                    photo.SaveAs(newfilePath);
                    sld.Photo = photo.FileName;
                }
                db.Slides.Add(sld);
                db.SaveChanges();
            }
            return RedirectToAction("index");
            
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Slide sld = db.Slides.Find(id);
            if (sld == null)
            {
                return HttpNotFound();
              
            }
            db.Slides.Remove(sld);
            db.SaveChanges();
            return RedirectToAction("index");
        }

        public ActionResult Edit(int? id) 
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Slide sld = db.Slides.Find(id);
            if (sld == null)
            {
                return HttpNotFound();
            }
            return View(sld);
        }

        [HttpPost]
        public ActionResult Edit(Slide sld, HttpPostedFileBase photo, string deletedphoto)
        {
            
            if (ModelState.IsValid)
            {
                if (photo != null && (photo.ContentType == "image/jpeg" || photo.ContentType == "image/png" || photo.ContentType == "image/jpg"))
                {
                    if (deletedphoto != null)
                    {
                        var fileDeletedPath = Path.Combine(Server.MapPath("~/Uploads/Slides"), deletedphoto);

                        System.IO.File.Delete(fileDeletedPath);
                    }
                  

                    var newfilePath = Path.Combine(HttpContext.Server.MapPath("~/Uploads/Slides"), photo.FileName);
                    photo.SaveAs(newfilePath);
                    sld.Photo = photo.FileName;

                }
            
                
                db.Entry(sld).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

            }

            return RedirectToAction("index", "slides");
        }
    }
}
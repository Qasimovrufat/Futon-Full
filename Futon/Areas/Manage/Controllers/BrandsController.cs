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
    public class BrandsController : Controller
    {
        FutonEntities1 db = new FutonEntities1();
        // GET: Manage/Brands
        public ActionResult Index()
        {
            List<Brand> brands = db.Brands.ToList();
            return View(brands);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Brand brd, HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                if (photo != null && (photo.ContentType == "image/jpeg" || photo.ContentType == "image/png" || photo.ContentType == "image/jpg"))
                {
                    var newfilePath = Path.Combine(HttpContext.Server.MapPath("~/Uploads/Brands"), photo.FileName);
                    photo.SaveAs(newfilePath);
                    brd.Logo = photo.FileName;
                }
                db.Brands.Add(brd);
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
            Brand brd = db.Brands.Find(id);
            if (brd == null)
            {
                return HttpNotFound();

            }
            db.Brands.Remove(brd);
            db.SaveChanges();
            return RedirectToAction("index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Brand brd = db.Brands.Find(id);
            if (brd == null)
            {
                return HttpNotFound();
            }
            return View(brd);
        }

        [HttpPost]
        public ActionResult Edit(Brand brd, HttpPostedFileBase photo, string deletedphoto)
        {

            if (ModelState.IsValid)
            {
                if (photo != null && (photo.ContentType == "image/jpeg" || photo.ContentType == "image/png" || photo.ContentType == "image/jpg"))
                {
                    if (deletedphoto != null)
                    {
                        var fileDeletedPath = Path.Combine(Server.MapPath("~/Uploads/Brands"), deletedphoto);

                        System.IO.File.Delete(fileDeletedPath);
                    }


                    var newfilePath = Path.Combine(HttpContext.Server.MapPath("~/Uploads/Brands"), photo.FileName);
                    photo.SaveAs(newfilePath);
                    brd.Logo = photo.FileName;

                }


                db.Entry(brd).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

            }

            return RedirectToAction("index", "Brands");
        }
    }
}
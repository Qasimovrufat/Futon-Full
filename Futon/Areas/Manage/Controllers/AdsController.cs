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
    public class AdsController : Controller
    {
        FutonEntities1 db = new FutonEntities1();
        // GET: Manage/Ads
        public ActionResult Index()
        {
            Adview data = new Adview();
            data.Ads = db.Ads.ToList();
            data.adLocations = db.AdLocations.ToList();
            return View(data);
        }

       

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Ad ad = db.Ads.Find(id);
            if (ad == null)
            {
                return HttpNotFound();
            }
            return View(ad);
        }

        [HttpPost]
        public ActionResult Edit(Ad ad, HttpPostedFileBase photo, string deletedphoto)
        {

            if (ModelState.IsValid)
            {
                if (photo != null && (photo.ContentType == "image/jpeg" || photo.ContentType == "image/png" || photo.ContentType == "image/jpg"))
                {
                    if (deletedphoto != null)
                    {
                        var fileDeletedPath = Path.Combine(Server.MapPath("~/Uploads"), deletedphoto);

                        System.IO.File.Delete(fileDeletedPath);
                    }


                    var newfilePath = Path.Combine(HttpContext.Server.MapPath("~/Uploads"), photo.FileName);
                    photo.SaveAs(newfilePath);
                    ad.Photo = photo.FileName;

                }


                db.Entry(ad).State = System.Data.Entity.EntityState.Modified;
                db.Entry(ad).Property(p => p.LocationId).IsModified = false;
                db.SaveChanges();

            }

            return RedirectToAction("index", "ads");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Futon.Models;
using Futon.Areas.Manage.Models;
using System.Data.Entity;
using Futon.Filter;

namespace Futon.Areas.Manage.Controllers
{
    [Auth]
    public class CategoriesController : Controller
    {
        FutonEntities1 db = new FutonEntities1();
        // GET: Manage/Categories
        public ActionResult Index()
        {
            List<Category> categories = db.Categories.ToList();
            return View(categories);
        }

        public ActionResult Create()
        {
            List<Department> departments = db.Departments.ToList();
            return View(departments);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Category ctg = db.Categories.Find(id);
            if (ctg == null)
            {
                return HttpNotFound();

            }
            db.Categories.Remove(ctg);
            db.SaveChanges();
            return RedirectToAction("index");
        }

        [HttpPost]
        public ActionResult Create(Category ctg)
        {
           
            if (ctg == null)
            {
                return HttpNotFound();
            }

            if (string.IsNullOrEmpty(ctg.Name))
            {
                Session["categoryError"] = "kateqoriya adi bos olammaz";
                return RedirectToAction("create");
            }
           
                db.Categories.Add(ctg);
                db.SaveChanges();
                return RedirectToAction("index");
            
          
        }

        public ActionResult Edit(int? id)
        {
          
            if (id == null)
            {
                return HttpNotFound();
            }
            ViewManageCategory data = new ViewManageCategory();
            data.category = db.Categories.Find(id);
            data.departments = db.Departments.ToList();
            if (data.category == null)
            {
                return HttpNotFound();
            }
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(Category ctg)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ctg).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return HttpNotFound();
        }
    }
}
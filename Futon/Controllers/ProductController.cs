using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Futon.Models;

namespace Futon.Controllers
{


    public class ProductController : Controller
    {
        FutonEntities1 db = new FutonEntities1();  
        // GET: Product
        public ActionResult Index(int id)
        {
            ViewProduct data = new ViewProduct {

                Product = db.Products.Find(id),
                
            };

            data.categories = db.Categories.Where(c => c.DepartmentId == data.Product.Category.DepartmentId).ToList();
            data.related = db.Products.Where(p => p.CategoryId == data.Product.CategoryId).OrderByDescending(p => p.Id).Take(10).ToList();
            if (data.Product == null)
            {
                return HttpNotFound();
            }

            return View(data);
        }

       
    }
}
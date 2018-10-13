using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Futon.Models;

namespace Futon.Controllers
{
    public class HomeController : Controller
    {
        FutonEntities1 db = new FutonEntities1();

        public ActionResult Index()
        {
            ViewHome data = new ViewHome
            {
                Slides = db.Slides.ToList(),
                Adds = db.Ads.ToList(),
                TopRated = db.Products.OrderByDescending(p => p.Reviews.Count()).Take(10).ToList(),
                NewArrivals = db.Products.OrderByDescending(p => p.CreatedAt).Take(10).ToList(),
                Featured = db.Products.OrderByDescending(p => p.view).Take(10).ToList(),
                SaleOff = db.Products.OrderByDescending(p => p.DiscountPrice != null).ToList(),
                Tags = db.Tags.Take(10).ToList(),
                Reviews = db.Reviews.ToList(),
            };

            ViewBag.Tags = db.Tags.ToList();
            return View(data);
        }

      
    }
}
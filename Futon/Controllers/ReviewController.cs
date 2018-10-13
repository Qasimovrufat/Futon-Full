using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Futon.Models;

namespace Futon.Controllers
{
    public class ReviewController : Controller
    {
        FutonEntities1 db = new FutonEntities1();
        // GET: Review
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Review rew,int id)
        {
            if (ModelState.IsValid)
            {
                rew.Rate = 5;
                rew.CreatedAt = DateTime.Now;
                rew.ProductId = id;
                db.Reviews.Add(rew);

                db.SaveChanges();
                return RedirectToAction("index","product",new {id=rew.ProductId });
            }
            return HttpNotFound();
        }
    }
}
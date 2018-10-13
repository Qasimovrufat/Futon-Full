using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Futon.Models;

namespace Futon.Controllers
{
    public class WishlistController : Controller
    {
        FutonEntities1 db = new FutonEntities1();
        // GET: Wishlist
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Add(int id)
        {
            Product prd = db.Products.Find(id);
            if (prd == null)
            {
                return Json(new
                {
                    status = 404
                }, JsonRequestBehavior.AllowGet);
            }
            if (Session["Wishlist"] == null)
            {
                Session["Wishlist"] = new List<Wishlist>();
            }

            List<Wishlist> Wishlists = Session["Wishlist"] as List<Wishlist>;

            if(Wishlists.Find(w=>w.Id == prd.Id) != null)
            {
                return Json(new
                {
                    status = 406,
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Wishlist wishlist = new Wishlist
                {
                    Id = prd.Id,
                    Qty = 1,
                    Name = prd.Name,
                    Price = prd.DiscountPrice != null ? (decimal)prd.DiscountPrice : prd.Price,
                    Photo = "/Uploads/Products/Thumbnail/" + prd.Photos.OrderBy(p => p.OrderBy).FirstOrDefault().Path,
                };
                Wishlists.Add(wishlist);
                
            }
            Session["Wishlist"] = Wishlists;

            return Json(new
            {
                status = 200,
                data = new
                {
               
                    list = Wishlists
                }
            }, JsonRequestBehavior.AllowGet);

        }
    }
}
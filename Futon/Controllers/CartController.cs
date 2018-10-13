using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Futon.Models;

namespace Futon.Controllers
{
    public class CartController : Controller
    {
        FutonEntities1 db = new FutonEntities1();
        // GET: Cart
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

            if (Session["Cart"] == null)
            {
                Session["Cart"] = new List<Cart>();
            }

            List<Cart> Carts = Session["Cart"] as List<Cart>;

            if (Carts.Find(c => c.Id == prd.Id) != null)
            {
                Carts.Find(c => c.Id == prd.Id).Qty++;
            }
            else
            {
                Cart cart = new Cart
                {
                    Id = prd.Id,
                    Qty = 1,
                    Name = prd.Name,
                    Price = prd.DiscountPrice != null ? (decimal)prd.DiscountPrice : prd.Price,
                    Photo = "/Uploads/Products/Thumbnail/" + prd.Photos.OrderBy(p => p.OrderBy).FirstOrDefault().Path
                };
                Carts.Add(cart);
            }

            Session["Cart"] = Carts;

            return Json(new
            {
                status = 200,
                data = new
                {
                    count = Carts.Sum(c => c.Qty),
                    total = Carts.Sum(c => c.Qty * c.Price).ToString("#.##"),
                    list = Carts
                }
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Remove(int id)
        {
            Product prd = db.Products.Find(id);
            if (prd == null)
            {
                return Json(new
                {
                    status = 404
                }, JsonRequestBehavior.AllowGet);
            }

            if (Session["Cart"] == null)
            {
                return Json(new
                {
                    status = 405
                }, JsonRequestBehavior.AllowGet);
            }

            List<Cart> Carts = Session["Cart"] as List<Cart>;

            if (Carts.Find(c => c.Id == prd.Id) == null)
            {
                return Json(new
                {
                    status = 406
                }, JsonRequestBehavior.AllowGet);
            }

            Carts.Remove(Carts.Find(c => c.Id == prd.Id));

            Session["Cart"] = Carts;

            return Json(new
            {
                status = 200,
                data = new
                {
                    count = Carts.Sum(c => c.Qty),
                    total = Carts.Sum(c => c.Qty * c.Price).ToString("#.##"),
                    list = Carts
                }
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeQty(int id,int qty)
        {
            Product prd = db.Products.Find(id);
            if (prd == null)
            {
                return Json(new
                {
                    status = 404
                }, JsonRequestBehavior.AllowGet);
            }

            if (Session["Cart"] == null)
            {
                return Json(new
                {
                    status = 405
                }, JsonRequestBehavior.AllowGet);
            }

            List<Cart> Carts = Session["Cart"] as List<Cart>;

            if (Carts.Find(c => c.Id == prd.Id) == null)
            {
                return Json(new
                {
                    status = 406
                }, JsonRequestBehavior.AllowGet);
            }

            Carts.Find(c => c.Id == prd.Id).Qty = qty;

            Session["Cart"] = Carts;

            return Json(new
            {
                status = 200,
                data = new
                {
                    count = Carts.Sum(c => c.Qty),
                    subtotal = (Carts.Find(c => c.Id == prd.Id).Qty * Carts.Find(c => c.Id == prd.Id).Price).ToString("#.##"),
                    total = Carts.Sum(c => c.Qty * c.Price).ToString("#.##"),
                    list = Carts
                }
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
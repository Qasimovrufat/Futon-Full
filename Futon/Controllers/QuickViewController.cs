using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Futon.Models;

namespace Futon.Controllers
{
    
    public class QuickViewController : Controller
    {
        FutonEntities1 db = new FutonEntities1();
        // GET: QuickView
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Show(int id)
        {
            Product prd = db.Products.Find(id);
            var listsize = new List<string>();
            var listColor = new List<object>();
            var listPhoto = new List<object>();
            if (prd == null)
            {
                return Json(new
                {
                    status = 404,
                }, JsonRequestBehavior.AllowGet);
            }
            
            foreach(var item in prd.ProductColorSizes.Where(pcs => pcs.ProductId == prd.Id && pcs.SizeId != null))
            {
                listsize.Add(item.Size.Name);
            }
            foreach (var item in prd.ProductColorSizes.Where(pcs => pcs.ProductId == prd.Id && pcs.ColorId != null))
            {
                listColor.Add(item.Color.HexCode);
            }
            foreach (var item in prd.Photos.OrderBy(p => p.OrderBy)){
                listPhoto.Add(item.Path);
            }
            return Json(new
            {
                status = 200,
                data = new
                {
                    name = prd.Name,
                    listsize,
                    listColor,
                    listPhoto, 
                    desc = prd.Description,
                    status = prd.Status,
                    price = prd.Price.ToString("#.##"),
                    discount = prd.DiscountPrice.Value.ToString("#.##"),
                    
                }

            }, JsonRequestBehavior.AllowGet);
        }
    }
}
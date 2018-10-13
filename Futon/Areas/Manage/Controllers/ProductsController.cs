using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Futon.Models;
using Futon.Areas.Manage.Models;
using Simple.ImageResizer;
using Futon.Filter;


namespace Futon.Areas.Manage.Controllers
{
    [Auth]
    public class ProductsController : Controller
    {
        FutonEntities1 db = new FutonEntities1();
        // GET: Manage/Products
        public ActionResult Index(string key,int page =1)
        {
            List<Product> products = db.Products.Where(p=>(key!=null&& key!=string.Empty)?p.Name.Contains(key):true).OrderByDescending(p => p.Id).Skip((page-1) * 20).Take(20).ToList();
            ViewBag.Total = Math.Ceiling(db.Products.Count() / 20.0);
            ViewBag.Key = key;
            ViewBag.Page = page;
            return View(products);
        }

        public ActionResult Create()
        {
            
            CreateProduct data = new CreateProduct()
            {
                Departments = db.Departments.ToList(),
                Brands = db.Brands.ToList(),
                Tags = db.Tags.ToList(),
                Colors = db.Colors.ToList(),
                Sizes = db.Sizes.ToList(),
            };    
            return View(data);
        }
        public JsonResult Categories(int id)
        {
            Department dpt = db.Departments.Find(id);

            if (dpt == null)
            {
                return Json(new
                {
                    status = 404,
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = 200,
                data = dpt.Categories.Select(c => new
                {
                    c.Id,
                    c.Name
                }).ToList()
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Upload()
        {
            var list = new List<object>();
            for (int i = 0; i < Request.Files.Count; i++)
            {
                string filename = DateTime.Now.ToString("yyyyMMddHHmmssss") + Request.Files[i].FileName;
                string path = System.IO.Path.Combine(Server.MapPath("~/Uploads/Products"), filename);
                Request.Files[i].SaveAs(path);
                
                ImageResizer resizer = new ImageResizer(path);
                resizer.Resize(168, 168, ImageEncoding.Jpg90);
                string thumPath = System.IO.Path.Combine(Server.MapPath("~/Uploads/Products/Thumbnail"), filename);
                resizer.SaveToFile(thumPath);

                var obj = new
                {
                    filename,
                    url = "/Uploads/Products/Thumbnail/" + filename
                };
                list.Add(obj);
            }

            return Json(new
            {
                status = 200,
                data = list
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RemoveFile(string filename)
        {
            string path = System.IO.Path.Combine(Server.MapPath("~/Uploads/Products"), filename);
            string thumPath = System.IO.Path.Combine(Server.MapPath("~/Uploads/Products/Thumbnail"), filename);
            System.IO.File.Delete(path);
            System.IO.File.Delete(thumPath);
            return Json(new
            {
                status = 200
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Create(Product prd)
        {
            prd.CreatedAt = DateTime.Now;

            if (prd.Name == string.Empty  || prd.Price == 0)
            {
                return Json(new
                {
                    status = 405,
                    message = "Mehsul adı,Kateqoriya və qiymət məcburdu"
                }, JsonRequestBehavior.AllowGet);
            }

            if (db.Products.FirstOrDefault(p => p.Name == prd.Name) != null)
            {
                return Json(new
                {
                    status = 408,
                    message = "Eyni adlı Məhsul var"
                }, JsonRequestBehavior.AllowGet);
            }

           

            if (prd.Photos.Count <2)
            {
                return Json(new
                {
                    status = 407,
                    message = "Məhsulun ən azı 2 şəkli olmalıdır"
                }, JsonRequestBehavior.AllowGet);
            }


            db.Products.Add(prd);
            db.SaveChanges();



            return Json(new
            {
                status = 200,
             

            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ToggleStatus(int id, bool status)
        {
            Product prd = db.Products.Find(id);
            if (prd == null)
            {
                return Json(new
                {
                    status = 404,
                    message = "Mehsul Tapılmadı"
                }, JsonRequestBehavior.AllowGet);
            }

            prd.Status = status;
            db.SaveChanges();
            return Json(new
            {
                status = 200
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int id)
        {
            Product prd = db.Products.Find(id);
            if (prd == null)
            {
                return HttpNotFound();
            }
            EditProduct model = new EditProduct
            {
                Departments = db.Departments.ToList(),
                Categories = db.Categories.ToList(),
                Product = prd,
                Tags = db.Tags.ToList(),
                Colors = db.Colors.ToList(),
                Sizes = db.Sizes.ToList(),
                Brands =db.Brands.ToList(),
            };


            return View(model);
        }

        [HttpPost]
        public JsonResult Edit(Product product, Photo[] Images, ProductTag[] Tags, ProductColorSize[] ColorSize) 
        {
            if (product.Name == string.Empty || product.CategoryId == 0 || product.Price == 0)
            {
                return Json(new
                {
                    status = 405,
                    message = "Mehsul adı,Kateqoriya və qiymət məcburdu"
                }, JsonRequestBehavior.AllowGet);
            }

       



            if (Images.Count() < 2)
            {
                return Json(new
                {
                    status = 407,
                    message = "Məhsulun ən azı 2 şəkli olmalıdır"
                }, JsonRequestBehavior.AllowGet);
            }



            db.Photos.RemoveRange(db.Photos.Where(p => p.ProductId == product.Id).ToList());
            db.ProductTags.RemoveRange(db.ProductTags.Where(tg => tg.ProductId == product.Id));
            db.ProductColorSizes.RemoveRange(db.ProductColorSizes.Where(tg => tg.ProductId == product.Id));
            db.SaveChanges();

            if (Tags != null)
            {
                db.ProductTags.AddRange(Tags);

            }
            if (ColorSize != null)
            {
                db.ProductColorSizes.AddRange(ColorSize);

            }
            db.Photos.AddRange(Images);

            db.Entry(product).State = System.Data.Entity.EntityState.Modified;
            db.Entry(product).Property(p => p.CreatedAt).IsModified = false;
            db.Entry(product).Property(p => p.BrandId).IsModified = false;
            db.SaveChanges();
            return Json(new {

                status = 200,

            },JsonRequestBehavior.AllowGet);


        }
    }
        


}
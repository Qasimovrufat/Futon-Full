using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Futon.Models;

namespace Futon.Controllers
{
    public class CategoryController : Controller
    {
        FutonEntities1 db = new FutonEntities1();
        // GET: Category
        public ActionResult Index(int id,string name,int? page,int?limit,string key,string range)
        {
            ViewCategory model = new ViewCategory {

                category = db.Categories.Find(id),
                Tags = db.Tags.OrderByDescending(t => t.Id).Take(10).ToList(),
                
            };
            if (limit == null)
            {
                limit = 12;
            };
            if(limit!=12 && limit != 24 && limit != 36)
            {
                return HttpNotFound();
            }
            if (page == null)
            {
                page = 1;
            }
            if (model.category == null)
            {
                return HttpNotFound();
            }

            if (model.category.Products.Count>1)
            {
                if (model.category.Products.Min(p => p.Price) > model.category.Products.Min(p => p.DiscountPrice))
                {
                    model.Min = model.category.Products.Min(p => p.DiscountPrice.Value);
                }
                else
                {
                    model.Min = model.category.Products.Min(p => p.Price);
                }
            }


            if (model.category.Products.Count > 1)
            {
                if (model.category.Products.Max(p => p.Price) < model.category.Products.Max(p => p.DiscountPrice))
                {
                    model.Max = model.category.Products.Max(p => p.DiscountPrice.Value);
                }
                else
                {
                    model.Max = model.category.Products.Max(p => p.Price);

                }
            }

            if (range != null)
            {
                if (range.Contains("-"))
                {
                    string[] price = range.Split('-');

                    if(!int.TryParse(price[0], out model.SltMin))
                    {
                        return HttpNotFound();
                    }
                    else
                    {
                        if (model.SltMin < model.Min)
                        {
                            return HttpNotFound();
                        }
                    }

                    if (!int.TryParse(price[1], out model.SltMax))
                    {
                        return HttpNotFound();
                    }
                    else
                    {
                        if (model.SltMax > model.Max)
                        {
                            return HttpNotFound();
                        }
                    }
                }
                else
                {
                    return HttpNotFound();
                }
            }
            model.Key = key;
            model.Page = (int)page;
            model.Limit = (int)limit;
            model.products = model.category.Products.Where(p=>(key!=null && key!=string.Empty)?p.Name.Contains(key):true && (model.SltMin != -1 ? (p.DiscountPrice!=null?p.DiscountPrice:p.Price) >= model.SltMin : true) && (model.SltMax != -1 ? (p.DiscountPrice != null ? p.DiscountPrice : p.Price) <= model.SltMax : true)&& p.Status).Skip(((int)page - 1)*(int)limit).Take((int)limit).ToList();
            model.Total = (int)Math.Ceiling(model.category.Products.Count() / (decimal)limit);
            model.Sizes = db.Sizes.Where(s => s.ProductColorSizes.Where(pcs => pcs.Product.CategoryId == model.category.Id).Count() > 0).ToList();

            if (model.Total > 0)
            {
                if (model.Page > model.Total)
                {
                    return HttpNotFound();
                }
            }

           
            return View(model);
        }
    }
}
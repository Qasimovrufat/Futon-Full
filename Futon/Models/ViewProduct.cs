using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Futon.Models;

namespace Futon.Models
{
    public class ViewProduct
    {
        public Product Product { get; set; }
        public List<Category> categories { get; set; }
        public List<Product> related { get; set; }
    }
}
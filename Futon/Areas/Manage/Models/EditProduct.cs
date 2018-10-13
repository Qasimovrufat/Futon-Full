using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Futon.Models;

namespace Futon.Areas.Manage.Models
{
    public class EditProduct:CreateProduct
    {
        public Product Product { get; set; }
        public List<Category> Categories { get; set; }
    }
}
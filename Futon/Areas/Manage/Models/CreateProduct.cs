using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Futon.Models;

namespace Futon.Areas.Manage.Models
{
    public class CreateProduct
    {
        public List<Department> Departments { get; set; }
        public List<Brand> Brands { get; set; }
        public List<Tag> Tags { get; set; }
        public List<Color> Colors { get; set; }
        public List<Size> Sizes { get; set; }
    }
}
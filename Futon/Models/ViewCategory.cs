using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Futon.Models;

namespace Futon.Models
{
    public class ViewCategory
    {
        public Category category { get; set; }
        public List<Product> products { get; set; }
        public List<Tag> Tags { get; set; }
        public List<Size> Sizes { get; set; }
        public int Total { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
        public string Key { get; set; }
        public decimal Min { get; set; }
        public decimal Max { get; set; }
        public int SltMin = -1;
        public int SltMax = -1;
        public List<ProductColorSize> pcs { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Futon.Models
{
    public class ViewHome
    {
        public List<Slide> Slides { get; set; }
        public List<Ad> Adds { get; set; }
        public List<Product> TopRated { get; set; }
        public List<Product> NewArrivals { get; set; }
        public List<Product> Featured { get; set; }
        public List<Product> SaleOff { get; set; }
        public List<Tag> Tags { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
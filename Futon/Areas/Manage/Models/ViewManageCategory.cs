using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Futon.Models;

namespace Futon.Areas.Manage.Models
{
    public class ViewManageCategory
    {
        public Category category { get; set; }
        public List<Department> departments { get; set; }
    }
}
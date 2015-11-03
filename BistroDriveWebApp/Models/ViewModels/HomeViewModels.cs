using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BistroDriveWebApp.Models
{
    public class AllDishListViewModel
    {
        public int Page { set; get; }
        public int PageCount { set; get; }
        public IEnumerable<dish> DishList { set; get; }
    }
}
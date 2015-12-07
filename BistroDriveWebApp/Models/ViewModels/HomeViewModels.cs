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
        public int CityId { set; get; }
        public IEnumerable<dishtype> DishTypes { set; get; }
        public IEnumerable<city> City_List { set; get; }
        public IEnumerable<dish> DishList { set; get; }
        public string DishType { set; get; }
        public string CanTeach { set; get; }
        public string Travel { set; get; }
        public int MinPrice { set; get; }
        public int MaxPrice { set; get; }
        public int MaxLimitPrice { set; get; }
        public int MinLimitPrice { set; get; }
    }

    public class UserListViewModel
    {
        public int Page { set; get; }
        public int PageCount { set; get; }
        public IEnumerable<aspnetuser> UserList { set; get; }
    }
}
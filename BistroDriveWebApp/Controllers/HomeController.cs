using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BistroDriveWebApp.Models;

namespace BistroDriveWebApp.Controllers
{
    public class HomeController : Controller
    {
        const int PageSize = 20;
        public ActionResult Index()
        {
            if (!Request.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Offers");
            }
        }

        public ActionResult Landing()
        {
            if (!Request.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Offers");
            }
        } 

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Offers(int page=0,string search = "", int CityId = 0,string DishType = "", 
            string canTeach = null, string travel = null, int minPrice = -1, int maxPrice = -1)
        {
            if (page < 0)
            {
                page = 0;
            }
            int dishcount = 0;
            var dlist = DataManager.Dish.GetDishList(page,PageSize, ref dishcount, search, CityId, DishType, canTeach != null, travel != null,minPrice,maxPrice);
            var cities = DataManager.Geolocation.GetAllCities();
            int minLimitPrice = DataManager.Dish.GetMinPrice();
            int maxLimitPrice = DataManager.Dish.GetMaxPrice();
            AllDishListViewModel model = new AllDishListViewModel
            {
                Page = page,
                DishList = dlist,
                DishTypes = DataManager.Dish.GetDishTypes(),
                PageCount = dishcount / PageSize,
                City_List = cities,
                CityId = CityId,
                DishType = DishType,
                CanTeach = canTeach,
                Travel = travel,
                MaxLimitPrice = maxLimitPrice,
                MinLimitPrice = minLimitPrice,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                Search = search
            };
            return View(model);
        }

        public ActionResult Users(int page = 0, string search = "", int CityId = 0, string travel = null)
        {
            if (page < 0)
            {
                page = 0;
            }
            int usercount = 0;
            var users = DataManager.User.GetAllUser(page, PageSize, ref usercount, search, CityId, travel != null);
            var cities = DataManager.Geolocation.GetAllCities();
            UserListViewModel model = new UserListViewModel
            {
                CityId = CityId,
                Page = page,
                PageCount = usercount/PageSize,
                Travel = travel,
                City_List = cities,
                UserList = users,
                Search = search
            };
            return View(model);
        }
    }
}
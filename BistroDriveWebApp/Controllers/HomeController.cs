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
            string canTeach = null, string travel = null, int minPrice = 0, int maxPrice = 0)
        {
            int dishcount = DataManager.Dish.GetDishCount();
            if (page > dishcount / PageSize)
            {
                page = dishcount / PageSize - 1;
            }
            if (page < 0)
            {
                page = 0;
            }
            var dlist = DataManager.Dish.GetDishList(page,PageSize, search, CityId, DishType, canTeach != null, travel != null,minPrice,maxPrice);
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
                MaxPrice = maxPrice
            };
            ViewBag.search = search;
            return View(model);
        }

        public ActionResult Users(int page = 0)
        {
            return View();
        }
    }
}
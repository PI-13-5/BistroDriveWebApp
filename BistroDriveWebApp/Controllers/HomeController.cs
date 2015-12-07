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

        public ActionResult Offers(int page=0)
        {
            int dishcount = DataManager.Dish.GetDishCount();
            if(page > dishcount / PageSize)
            {
                page = dishcount / PageSize - 1;
            }
            if (page < 0)
            {
                page = 0;
            }
            var dlist = DataManager.Dish.GetDishList(page,PageSize);
            AllDishListViewModel model = new AllDishListViewModel
            {
                Page = page,
                DishList = dlist,
                PageCount = dishcount/PageSize
            };
            return View(model);
        }
    }
}
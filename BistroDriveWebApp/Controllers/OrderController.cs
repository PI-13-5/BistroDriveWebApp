using BistroDriveWebApp.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BistroDriveWebApp.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateOrder(int id = -1)
        {
            var dish = DataManager.Dish.GetDishById(id);
            if (dish == null)
            {
                return RedirectToAction("index", "home");
            }

            var userId = User.Identity.GetUserId();
            var user = DataManager.User.GetUserById(userId);

            var seller = DataManager.User.GetUserById(dish.Id_User);

            OrderViewModel model = new OrderViewModel();


            return View(model);
        }
    }
}
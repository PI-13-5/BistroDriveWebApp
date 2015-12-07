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

        [HttpGet]
        public ActionResult MakeOrder(int id = -1)
        {
            var dish = DataManager.Dish.GetDishById(id);
            if (dish == null)
            {
                return RedirectToAction("index", "home");
            }

            var userId = User.Identity.GetUserId();
            var cook = DataManager.User.GetUserById(dish.Id_User);

            if (userId == cook.Id)
            {
                return RedirectToAction("index", "home");
            }

            var user = DataManager.User.GetUserById(userId);
            var seller = DataManager.User.GetUserById(dish.Id_User);
            //списки
            ViewBag.Payments = DataManager.PaymentMethod.GetPaymentMethods();
            ViewBag.Deliveries = DataManager.Delivery.GetDeliveryMethods();
            ViewBag.Contacts = DataManager.Contact.GetContactMethods();
            ViewBag.IngridientsBuyers = DataManager.IngridientsBuyer.GetIngridientsBuyers();
            var cities = DataManager.Geolocation.GetAllCities();
            //
            OrderViewModel model = new OrderViewModel
            {
                Address = user == null ? "" : user.Address,
                Email = user == null ? "" : user.Email,
                Phone = user == null ? "" : user.PhoneNumber,
                FirstName = user == null ? "" : user.FristName,
                Surname = user == null ? "" : user.Surname,
                City = user == null ? 0 : user.Id_City,
                idDish = dish.Id_Dish,
                CookEmail = cook.Email,
                CookName = cook.FristName,
                CookSurname = cook.Surname,
                CookPhone = cook.PhoneNumber,
                idCook = dish.Id_User,
                DishName = dish.Name,
                DishPrice = dish.Price,
                DishPriceWithIngridient = dish.PriceWithIngridient,
                DishDescription = dish.Description,
                ImageUrl = dish.ImageUrl,
                DeadLine = DateTime.Now.AddHours(2),
                City_List = cities
            };


            return View(model);
        }

        [HttpPost]
        public ActionResult MakeOrder(OrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Payments = DataManager.PaymentMethod.GetPaymentMethods();
                ViewBag.Deliveries = DataManager.Delivery.GetDeliveryMethods();
                ViewBag.Contacts = DataManager.Contact.GetContactMethods();
                ViewBag.IngridientsBuyers = DataManager.IngridientsBuyer.GetIngridientsBuyers();
                return View(model);
            }
            var userId = User.Identity.GetUserId();
            var cook = DataManager.User.GetUserById(model.idCook);
            var dish = DataManager.Dish.GetDishById(model.idDish);
            //защита от скул хакеров
            if(cook==null || dish == null || userId == cook.Id)
            {
                return RedirectToAction("index", "home");
            }

            order result = new order
            {
                Address = model.Address,
                Comment = model.Comment,
                CreateTime = DateTime.Now,
                id_IngridientsBuyer = model.ingridientBuyer,
                DeadLine = model.DeadLine,
                Email = model.Email,
                FirstName = model.FirstName,
                Id_ContactMethod = model.contactMethod,
                Id_Cook = model.idCook,
                Id_Customer = userId,
                Id_Delivery = model.delivery,
                Id_PaymentMethod = model.paymentMethod,
                Id_Status = 1,// новый
                Phone = model.Phone,
                Surname = model.Surname,
                id_city = model.City
            };
            List<orderproduct> resultProducts = new List<orderproduct>();
            resultProducts.Add(new orderproduct {
                Id_Dish = dish.Id_Dish,
                Price = dish.Price,
                PriceWithIngridients = dish.PriceWithIngridient
            });
            DataManager.Order.AddOrder(result, resultProducts);
            return RedirectToAction("success");
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}
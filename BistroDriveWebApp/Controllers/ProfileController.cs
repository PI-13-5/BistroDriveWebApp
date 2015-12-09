using BistroDriveWebApp.Core;
using BistroDriveWebApp.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BistroDriveWebApp.Controllers
{
    public class ProfileController : Controller
    {

        // GET: Profile
        public ActionResult Index(string id)
        {
            aspnetuser user = GetUser(id);
            if(user == null)
            {
                return RedirectToAction("index", "home");
            }

            string userId = user.Id;

            var description = DataManager.User.GetUserDescription(userId);
            IEnumerable<dish> dishList = DataManager.Dish.GetDishByUserId(userId, 5);
            var review = DataManager.User.GetReviewByUserId(userId);
            var model = new ProfileViewModel
            {
                Id = userId,
                Avatar_Url = user.Avatar_Url,
                Firstname = user.FristName,
                Surname = user.Surname,
                UserName = user.UserName,
                Description = description,
                Rating = user.Raiting == null? 0:(int)user.Raiting,
                DishList = dishList,
                ReviewList = review
            };
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddDish()
        {
            ViewBag.Types = DataManager.Dish.GetDishTypes();
            DishViewModel model = new DishViewModel();
            return View(model);
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddDish(DishViewModel model, HttpPostedFileBase image)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Types = DataManager.Dish.GetDishTypes();
                return View(model);
            }
            if (image != null && image.ContentLength > 0)
            {
                // узнаём тип файла
                string fileName = SaveFile(image, "/Uploads/dish");
                model.Image_Url = "/Uploads/dish/" + fileName;
            }
            string idUser = User.Identity.GetUserId();
            // формируем новые данные
            dish item = new dish
            {
                Id_User = idUser,
                Id_Type = model.Id_Type,
                Name = model.Name,
                Description = model.Description,
                ImageUrl = model.Image_Url,
                Price = model.Price,
                PriceWithIngridient = model.PriceWithIngridients,
                Ingridient = model.Ingridients,
                CanTeach = model.CanTeach != null
            };
            //записуем изменения в БД
            DataManager.Dish.AddDish(item);
            return RedirectToAction("index");
        }

        [Authorize]
        public ActionResult DeleteDish(int id)
        {
            dish item = DataManager.Dish.GetDishById(id);
            //защита от редактирования чужих блюд
            if (item.Id_User != User.Identity.GetUserId())
            {
                return RedirectToAction("index");
            }
            var old_file = item.ImageUrl;
            //чистим с БД
            DataManager.Dish.Delete(id);
            //удаляем изображение
            if (old_file != null)
            {
                var path = Server.MapPath(old_file);
                FileInfo fi = new FileInfo(path);
                try
                {
                    fi.Delete();
                }
                catch { }
            }
            return RedirectToAction("dish");
        }

        public ActionResult DishInfo(int id)
        {
            dish item = DataManager.Dish.GetDishById(id);
            if (item == null)
            {
                return RedirectToAction("Index","home");
            }
            aspnetuser user = DataManager.User.GetUserById(item.Id_User);
            ViewBag.Username = user.UserName;
            ViewBag.Review = DataManager.Dish.GetDishReviewByDishId(id);
            return View(item);
        }

        [Authorize]
        [HttpGet]
        public ActionResult EditDish(int Id)
        {
            dish item = DataManager.Dish.GetDishById(Id);
            //защита от редактирования чужих блюд
            if (item.Id_User != User.Identity.GetUserId())
            {
                return RedirectToAction("index");
            }

            ViewBag.Types = DataManager.Dish.GetDishTypes();
            if(item == null)
            {
                return RedirectToAction("index");
            }
            DishViewModel model = new DishViewModel
            {
                Id_Dish = item.Id_Dish,
                Description = item.Description,
                Id_Type = item.Id_Type,
                Image_Url = item.ImageUrl,
                Ingridients = item.Ingridient,
                Name = item.Name,
                Price = item.Price,
                PriceWithIngridients = item.PriceWithIngridient,
                CanTeach = item.CanTeach == false ? null : "true"
            };

            return View(model);
        }

        [Authorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditDish(DishViewModel model, HttpPostedFileBase image)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Types = DataManager.Dish.GetDishTypes();
                return View(model);
            }
            if (image != null && image.ContentLength > 0)
            {
                // узнаём тип файла
                string fileName = SaveFile(image, "/Uploads/dish",model.Image_Url);
                model.Image_Url = "/Uploads/dish/" + fileName;
            }
            string idUser = User.Identity.GetUserId();
            //если кто-то захочет отредактировать без прав
            if(idUser != DataManager.Dish.GetDishOwner(model.Id_Dish))
            {
                return RedirectToAction("index");
            }
            // формируем новые данные
            DataManager.Dish.UpdateDish(model);
            return RedirectToAction("Dish");
        }

        /// <summary>
        /// Сохроняет файл с HttpPost потока
        /// </summary>
        /// <param name="file">HttpPost поток</param>
        /// <param name="folder">Папка сохранения</param>
        /// <param name="old_file">Имя старого файла, если необходимо его удалить</param>
        /// <returns>Имя сохранённого файла</returns>
        private string SaveFile(HttpPostedFileBase file, string folder, string old_file = null)
        {
            int typeBegin = file.FileName.LastIndexOf('.');
            string fileType = file.FileName.Substring(typeBegin, file.FileName.Length - typeBegin);
            //генерируем уникальное имя
            var fileName = Security.GetMd5(User.Identity.GetUserId() + DateTime.Now.ToFileTimeUtc()) + fileType;
            var path = Path.Combine(Server.MapPath(folder), fileName);
            file.SaveAs(path);
            if (old_file != null)
            {
                path = Server.MapPath(old_file);
                FileInfo fi = new FileInfo(path);
                try
                {
                    fi.Delete();
                }
                catch { }
            }
            return fileName;
        }
        
        public ActionResult Dish(string id)
        {
            aspnetuser user = GetUser(id);
            if (user == null)
            {
                return RedirectToAction("index", "home");
            }
            var userId = user.Id;
            var dishList = DataManager.Dish.GetDishByUserId(userId);
            DishListViewModel model = new DishListViewModel
            {
                Firstname = user.FristName,
                Surname = user.Surname,
                UserName = user.UserName,
                DishList = dishList,
                WithTravel = user.withTravel
            };
            return View(model);
        }

        /// <summary>
        /// Получение запрашиваемого пользователя, если запрос несуществующий возвратит Null
        /// </summary>
        /// <param name="userName">Запрашиваемое имя пользователя</param>
        /// <returns></returns>
        private aspnetuser GetUser(string userName = null)
        {
            if (userName != null)
            {
                return DataManager.User.GetUserByName(userName);
            }
            else
            {
                string currentUserId = User.Identity.GetUserId();
                return DataManager.User.GetUserById(currentUserId);
            }
        }

        [Authorize]
        public ActionResult Order()
        {
            var user = GetUser();
            IEnumerable<order> iorders = DataManager.Order.GetOrdersByUserId(user.Id, true);
            IEnumerable<order> oorders = DataManager.Order.GetOrdersByUserId(user.Id, false);
            OrderListViewModel model = new OrderListViewModel
            {
                Firstname = user.FristName,
                Surname = user.Surname,
                UserName = user.UserName,
                IncommingOrderList = iorders,
                OutcommingOrderList = oorders
            };
            return View(model);
        }

        [Authorize]
        public ActionResult OrderInfo(int id = -1)
        {
            var user = GetUser();
            order o = DataManager.Order.GetOrderById(id);
            if(o == null)
            {
                return RedirectToAction("index", "home");
            }
            if(o.Id_Customer != user.Id && o.Id_Cook != user.Id)
            {
                return RedirectToAction("index", "home");
            }
            ViewBag.Statuses = DataManager.Status.GetOrderStatuses();
            var customer = DataManager.User.GetUserById(o.Id_Customer);
            var productList = DataManager.Order.GetOrderProducts(o.Id_Order);
            OrderDitailViewModel model = new OrderDitailViewModel
            {
                Order = o,
                Customer = customer,
                ProductsList = productList
            };
            return View(model);
        }

        [Authorize]
        public ActionResult SetOrderStatus(OrderDitailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Order");
            }

            aspnetuser user = GetUser();
            order o = DataManager.Order.GetOrderById(model.Order.Id_Order);
            if(o.Id_Cook != user.Id && o.Id_Customer != user.Id)
            {
                return RedirectToAction("Index", "Home");
            }

            DataManager.Order.UpdateStatus(model.Order.Id_Order, model.Order.Id_Status);
            return RedirectToAction("Order");
        }

        [Authorize]
        public ActionResult CloseOrder(OrderDitailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Order");
            }

            aspnetuser user = GetUser();
            order o = DataManager.Order.GetOrderById(model.Order.Id_Order);
            if (o.Id_Cook != user.Id && o.Id_Customer != user.Id)
            {
                return RedirectToAction("Index", "Home");
            }

            DataManager.Order.CloseOrder(model.Order.Id_Order);
            return RedirectToAction("Order");
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddDishFeedback(int id)
        {
            string user = User.Identity.GetUserId();
            dish d = DataManager.Dish.GetDishById(id);
            if(d == null || user == d.Id_User)
            {
                return RedirectToAction("index", "profile");
            }
            DishReviewViewModel model = new DishReviewViewModel
            {
                ID_Dish = d.Id_Dish,
                DishName = d.Name,
                Image_Url = d.ImageUrl 
            };
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddDishFeedback(DishReviewViewModel model)
        {
            string user = User.Identity.GetUserId();
            dish d = DataManager.Dish.GetDishById(model.ID_Dish);
            if (d == null || user == d.Id_User)
            {
                return RedirectToAction("index", "profile");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            DataManager.Dish.AddDishReview(user, model.ID_Dish, model.Mark, model.Review);
            return RedirectToAction("dishinfo", "profile", new { id = model.ID_Dish });
        }

        [Authorize]
        public ActionResult DeleteDishReview(int id)
        {
            string user = User.Identity.GetUserId();
            dishreview review = DataManager.Dish.GetDishReviewById(id);
            int id_dish = review.Id_Dish;
            if (review == null || user != review.Id_Owner)
            {
                return RedirectToAction("index", "profile");
            }
            DataManager.Dish.DeleteDishReview(id);
            return RedirectToAction("dishinfo", "profile", new { id = id_dish });
        }

        [Authorize]
        [HttpGet]
        public ActionResult EditDishReview(int id)
        {
            string user = User.Identity.GetUserId();
            dishreview review = DataManager.Dish.GetDishReviewById(id);
            if (review == null || user != review.Id_Owner)
            {
                return RedirectToAction("index", "profile");
            }
            var dish = DataManager.Dish.GetDishById(review.Id_Dish);
            DishReviewViewModel model = new DishReviewViewModel
            {
                ID_Dish = review.Id_Dish,
                DishName = dish.Name,
                Image_Url = dish.ImageUrl,
                ID_Review = review.Id_Review,
                Mark = (int)review.Mark,
                Review = review.Description
            };
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditDishReview(DishReviewViewModel model)
        {
            string user = User.Identity.GetUserId();
            dishreview review = DataManager.Dish.GetDishReviewById(model.ID_Review);
            if (review == null || user != review.Id_Owner)
            {
                return RedirectToAction("index", "profile");
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            DataManager.Dish.EditDishReview(model.ID_Review, model.Mark, model.Review);
            return RedirectToAction("dishinfo", "profile", new { id = model.ID_Dish });
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddUserReview(string id)
        {
            string curruser = User.Identity.GetUserId();
            aspnetuser user = DataManager.User.GetUserByName(id);
            if (user == null || curruser == user.Id)
            {
                return RedirectToAction("index", "profile");
            }

            UserReviewViewModel model = new UserReviewViewModel
            {
                ID_user = user.Id,
                Avatar = user.Avatar_Url,
                UserName = user.UserName,
                FirstName = user.FristName,
                SurName = user.Surname
            };
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddUserReview(UserReviewViewModel model)
        {
            string curruser = User.Identity.GetUserId();
            aspnetuser user = DataManager.User.GetUserByName(model.UserName);
            if (user == null || curruser == user.Id)
            {
                return RedirectToAction("index", "profile");
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            DataManager.User.AddUserReview(model.ID_user, curruser, model.Mark, model.Review);
            return RedirectToAction("index", "profile", new { id = model.UserName });
        }

        [Authorize]
        public ActionResult DeleteReview(int id)
        {
            string user = User.Identity.GetUserId();
            review r = DataManager.User.GetReviewById(id);
            if (r == null)
            {
                return RedirectToAction("index", "profile");
            }
            string userid = r.Id_User;
            string username = r.aspnetuser1.UserName;
            if (user != r.Id_Owner)
            {
                return RedirectToAction("index", "profile");
            }
            DataManager.User.DeleteUserReview(id);
            return RedirectToAction("index", "profile", new { id = username });
        }
    }
}
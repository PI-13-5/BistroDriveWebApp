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
            var model = new ProfileViewModel
            {
                Id = userId,
                Avatar_Url = user.Avatar_Url,
                Firstname = user.FristName,
                Surname = user.Surname,
                UserName = user.UserName,
                Description = description,
                Rating = 5,
                DishList = dishList
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
                Ingridient = model.Ingridients
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
                PriceWithIngridients = item.PriceWithIngridient
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
                DishList = dishList
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
    }
}
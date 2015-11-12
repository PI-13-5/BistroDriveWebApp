using BistroDriveWebApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace BistroDriveWebApp.Controllers
{
    public class ApiController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: Api
        public async Task<ActionResult> Index()
        {
            var jsonString = String.Empty;

            Request.InputStream.Position = 0;
            using (var inputStream = new StreamReader(Request.InputStream))
            {
                jsonString = inputStream.ReadToEnd();
            }
            if (jsonString != "")
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                JsonRequestBody serJsonDetails = null;
                try {
                    serJsonDetails = (JsonRequestBody)javaScriptSerializer.Deserialize(jsonString, typeof(JsonRequestBody));
                }
                catch
                {
                    serJsonDetails = new JsonRequestBody();
                }
                //если параметры не переданы то инициализируем пустым значением
                if (serJsonDetails.Parameters == null)
                {
                    serJsonDetails.Parameters = new Dictionary<string, string>();
                }
                JsonRespondBody result = await RedirectJsonString(serJsonDetails);
                string output = javaScriptSerializer.Serialize(result);
                Response.Clear();
                Response.ContentType = "application/json; charset=utf-8";
                Response.ContentEncoding = Encoding.UTF8;
                Response.Write(output);
                Response.End();
            }
            return RedirectToAction("index", "home");
        }

        //переадресовывает запрос на нужный метод, возвращает - JSON строку
        private async Task<JsonRespondBody> RedirectJsonString(JsonRequestBody json)
        {
            if(String.Compare(json.Method,"login", true)==0)
            {
                return await LoginMethod(json);
            }
            //Далее методы которым нужен токен

            if (String.Compare(json.Method, "profile", true) == 0)
            {
                return ProfileMethod(json);
            }
            else if (String.Compare(json.Method, "dishlist", true) == 0)
            {
                return DishlistMethod(json);
            }
            else if (String.Compare(json.Method, "dishinfo", true) == 0)
            {
                return DishinfoMethod(json);
            }
            else if (String.Compare(json.Method, "offers", true) == 0)
            {
                return OffersMethod(json);
            }
            else if (String.Compare(json.Method, "orderinfo", true) == 0)
            {
                return OrderInfoMethod(json);
            }
            else if (String.Compare(json.Method, "orderlist", true) == 0)
            {
                return OrderListMethod(json);
            }
            JsonRespondBody result = new JsonRespondBody { Error = "Invalid method", Status = "error" };
            return result;
        }

        private JsonRespondBody OrderListMethod(JsonRequestBody json)
        {
            aspnetuser user = DataManager.User.GetUserByToken(json.Token);
            bool incomingOrders = true;
            if (user == null)
            {
                return new JsonRespondBody { Error = "Invalid token", Status = "error" };
            }
            if (json.Parameters.ContainsKey("incomingOrders"))
            {
                try
                {
                    incomingOrders = Convert.ToBoolean(json.Parameters["incomingOrders"]);
                }
                catch
                {
                    return new JsonRespondBody { Error = "Invalid parameters", Status = "error" };
                }
            }

            DataManager.User.RefreshBuffer();
            IEnumerable<order> ord = DataManager.Order.GetOrdersByUserId(user.Id, incomingOrders);
            List<OrderSerializerBody> orders = new List<OrderSerializerBody>();
            foreach(var item in ord)
            {
                orders.Add(GetOrderInformation(item));
            }
            JsonRespondBody result = new JsonRespondBody { Status = "OK", Result = orders };
            return result;
        }

        private JsonRespondBody OrderInfoMethod(JsonRequestBody json)
        {
            aspnetuser user = DataManager.User.GetUserByToken(json.Token);
            int id_order = 0;
            if (user == null)
            {
                return new JsonRespondBody { Error = "Invalid token", Status = "error" };
            }
            if (!json.Parameters.ContainsKey("Id_Order"))
            {
                return new JsonRespondBody { Error = "Id_Order is required", Status = "error" };
            }
            try
            {
                id_order = Convert.ToInt32(json.Parameters["Id_Order"]);
            }
            catch
            {
                return new JsonRespondBody { Error = "Invalid parameters", Status = "warning" };
            }
            order o = DataManager.Order.GetOrderById(id_order);
            if (o == null)
            {
                return new JsonRespondBody { Error = "Order not found", Status = "warning" };
            }
            if (o.Id_Cook != user.Id && o.Id_Customer != user.Id)
            {
                return new JsonRespondBody { Error = "Access denied", Status = "warning" };
            }
            //формируем ответ
            string address = string.Format("{0}://{1}", Request.Url.Scheme, Request.Url.Authority);
            IEnumerable<orderproduct> op = DataManager.Order.GetOrderProducts(o.Id_Order);
            List<DishSerealizerBody> dl = new List<DishSerealizerBody>();
            foreach (var item in op)
            {
                dl.Add(new DishSerealizerBody
                {
                    Id = item.Id_Dish,
                    Name = item.dish.Name,
                    Price = item.Price,
                    PriceWithIngridients = item.PriceWithIngridients,
                    Image = address + item.dish.ImageUrl
                });
            }
            DataManager.User.RefreshBuffer();
            OrderSerializerBody res = GetOrderInformation(o);
            res.OrderList = dl;
            JsonRespondBody result = new JsonRespondBody { Status = "OK", Result = res };
            return result;
        }

        private OrderSerializerBody GetOrderInformation(order o)
        {
            string address = string.Format("{0}://{1}", Request.Url.Scheme, Request.Url.Authority);
            UserSerealizerBody cook = GetUserInfoBuffered(o.Id_Cook);
            aspnetuser cust = DataManager.User.GetUserByIdBuffered(o.Id_Customer);
            UserSerealizerBody customer = new UserSerealizerBody
            {
                Id = o.Id_Customer,
                Email = o.Email,
                Address = o.Address,
                FirstName = o.FirstName,
                Surname = o.Surname,
                Phone = o.Phone,
                Username = cust == null ? null : cust.UserName,
                AvatarUrl = cust == null ? null : (address + cust.Avatar_Url)
            };
            OrderSerializerBody res = new OrderSerializerBody
            {
                Comment = o.Comment,
                Comunication = o.ordercontactmethod.Name,
                Deadline = o.DeadLine == null? null : ConvertToUnixTime((DateTime)o.DeadLine).ToString(),
                Delivery = o.orderdelivery.Name,
                FinishTime = o.FinishTime == null ? null : ConvertToUnixTime((DateTime)o.FinishTime).ToString(),
                Payment = o.orderpaymentmethod.Name,
                Status = o.orderstatu.Name,
                Cook = cook,
                Id_Order = o.Id_Order,
                Total = o.total,
                IngridientsBuyer = o.orderingridientbuyer.Name,
                Customer = customer
            };
            return res;
        }

        public static long ConvertToUnixTime(DateTime datetime)
        {
            DateTime sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return (long)(datetime - sTime).TotalSeconds;
        }

        private JsonRespondBody OffersMethod(JsonRequestBody json)
        {
            int Limit = 20;
            int Page = 0;
            aspnetuser user = DataManager.User.GetUserByToken(json.Token);
            if (user == null)
            {
                return new JsonRespondBody { Error = "Invalid token", Status = "error" };
            }
            if(json.Parameters.ContainsKey("Limit"))
            {
                try
                {
                    Limit = Convert.ToInt32(json.Parameters["Limit"]);
                    Limit = Limit < 1 ? 1 : Limit;
                }
                catch { }
            }
            if (json.Parameters.ContainsKey("Page"))
            {
                try
                {
                    Page = Convert.ToInt32(json.Parameters["Page"]);
                    Page = Page < 0 ? 0 : Page;
                }
                catch { }
            }
            //генерация ответа
            string address = string.Format("{0}://{1}", Request.Url.Scheme, Request.Url.Authority);
            IEnumerable<dish> list = DataManager.Dish.GetDishList(Page, Limit);
            List<DishSerealizerBody> res = new List<DishSerealizerBody>();
            foreach(var item in list)
            {
                res.Add(new DishSerealizerBody
                {
                    Description = item.Description,
                    Id = item.Id_Dish,
                    Ingridients = item.Ingridient,
                    Name = item.Name,
                    Price = item.Price,
                    PriceWithIngridients = item.PriceWithIngridient,
                    Type = item.dishtype.Name,
                    Image = address + item.ImageUrl
                });
            }
            OfferSerealizerBody r = new OfferSerealizerBody
            {
                CurrentPage = Page,
                Limit = Limit,
                List = res,
            };
            JsonRespondBody result = new JsonRespondBody { Status = "OK", Result = r };
            return result;
        }

        private JsonRespondBody DishinfoMethod(JsonRequestBody json)
        {
            aspnetuser user = DataManager.User.GetUserByToken(json.Token);
            if (user == null)
            {
                return new JsonRespondBody { Error = "Invalid token", Status = "error" };
            }
            if (!json.Parameters.ContainsKey("Id_Dish"))
            {
                 return new JsonRespondBody { Error = "Id_Dish is required", Status = "error" };
            }
            int dish_id = 0;
            try
            {
                dish_id = Convert.ToInt32(json.Parameters["Id_Dish"]);
            }
            catch
            {
                return new JsonRespondBody { Error = "Invalid parameters", Status = "error" };
            }
            //генерация ответа
            dish d = DataManager.Dish.GetDishById(dish_id);
            if (d == null)
            {
                return new JsonRespondBody { Error = "Dish not found", Status = "warning" };
            }
            string address = string.Format("{0}://{1}", Request.Url.Scheme, Request.Url.Authority);
            DishSerealizerBody res = new DishSerealizerBody
            {
                Description = d.Description,
                Id = d.Id_Dish,
                Ingridients = d.Ingridient,
                Name = d.Name,
                Price = d.Price,
                PriceWithIngridients = d.PriceWithIngridient,
                Type = d.dishtype.Name,
                Image = address + d.ImageUrl
            };
            JsonRespondBody result = new JsonRespondBody
            {
                Result = res,
                Status = "OK"
            };
            return result;

        }

        private JsonRespondBody DishlistMethod(JsonRequestBody json)
        {
            aspnetuser user = DataManager.User.GetUserByToken(json.Token);
            if (user == null)
            {
                return new JsonRespondBody { Error = "Invalid token", Status = "error" };
            }
            if (json.Parameters.ContainsKey("username"))
            {
                user = DataManager.User.GetUserByName(json.Parameters["username"]);
                if (user == null)
                {
                    return new JsonRespondBody { Error = "Invalid username", Status = "error" };
                }
            }

            //генерация ответа
            List<DishSerealizerBody> reslist = GetDishList(user.Id);
            JsonRespondBody result = new JsonRespondBody
            {
                Result = reslist,
                Status = "OK"
            };
            return result;
        }

        private List<DishSerealizerBody> GetDishList(string id_user)
        {
            string address = string.Format("{0}://{1}", Request.Url.Scheme, Request.Url.Authority);
            IEnumerable<dish> list = DataManager.Dish.GetDishByUserId(id_user);
            List<DishSerealizerBody> reslist = new List<DishSerealizerBody>();
            foreach (var item in list)
            {
                reslist.Add(new DishSerealizerBody
                {
                    Description = item.Description,
                    Id = item.Id_Dish,
                    Ingridients = item.Ingridient,
                    Name = item.Name,
                    Price = item.Price,
                    PriceWithIngridients = item.PriceWithIngridient,
                    Type = item.dishtype.Name,
                    Image = address + item.ImageUrl
                });
            }
            return reslist;
        }

        private JsonRespondBody ProfileMethod(JsonRequestBody json)
        {
            aspnetuser user = DataManager.User.GetUserByToken(json.Token);
            if (user == null)
            {
                return new JsonRespondBody { Error = "Invalid token", Status = "error" };
            }
            if (json.Parameters.ContainsKey("username"))
            {
                user = DataManager.User.GetUserByName(json.Parameters["username"]);
                if (user == null)
                {
                    return new JsonRespondBody { Error = "Invalid username", Status = "error" };
                }
            }

            //генерация ответа
            UserSerealizerBody ub = GetUserInfo(user.Id);
            JsonRespondBody result = new JsonRespondBody
            {
                Result = ub,
                Status = "OK"
            };
            return result;
        }

        private async Task<JsonRespondBody> LoginMethod(JsonRequestBody json)
        {
            if (json.Parameters.ContainsKey("login") == false || json.Parameters.ContainsKey("password") == false)
            {
                return new JsonRespondBody { Status = "error", Error = "Invalid parameters" };
            }
            string login = json.Parameters["login"];
            string password = json.Parameters["password"];
            string uname = login;
            //проверка логина и пароля на корректность
            var singingresult = await SignInManager.PasswordSignInAsync(login, password, false, false);
            if (singingresult == SignInStatus.Failure)
            {
                var suser = await UserManager.FindByEmailAsync(login);
                if (suser == null)
                {
                    return new JsonRespondBody { Status = "Failure", Error = "Invalid login or password" };
                }
                singingresult = await SignInManager.PasswordSignInAsync(suser.UserName, password, false, false);
                if (singingresult == SignInStatus.Failure)
                {
                    return new JsonRespondBody { Status = "Failure", Error = "Invalid login or password" };
                }
                uname = suser.UserName;
            }
            var user = DataManager.User.GetUserByName(uname);
            JsonRespondBody result = new JsonRespondBody { Status = "OK" };
            //формируем результат
            usertoken token = DataManager.User.GenerateToken(user.Id);
            UserSerealizerBody ub = GetUserInfo(user.Id);
            ub.Token = token.Token;
            result.Result = ub;
            return result;
        }

        public UserSerealizerBody GetUserInfo(string userId)
        {

            var user = DataManager.User.GetUserById(userId);
            string description = DataManager.User.GetUserDescription(userId);
            string address = string.Format("{0}://{1}", Request.Url.Scheme, Request.Url.Authority);
            UserSerealizerBody result = new UserSerealizerBody
            {
                Id = user.Id,
                Email = user.Email,
                Address = user.Address,
                Description = description,
                FirstName = user.FristName,
                Surname = user.Surname,
                Phone = user.PhoneNumber,
                Username = user.UserName,
                AvatarUrl = address + user.Avatar_Url
            };
            return result;
        }
        public UserSerealizerBody GetUserInfoBuffered(string userId)
        {

            var user = DataManager.User.GetUserByIdBuffered(userId);
            string description = DataManager.User.GetUserDescriptionBuffered(userId);
            string address = string.Format("{0}://{1}", Request.Url.Scheme, Request.Url.Authority);
            UserSerealizerBody result = new UserSerealizerBody
            {
                Id = user.Id,
                Email = user.Email,
                Address = user.Address,
                Description = description,
                FirstName = user.FristName,
                Surname = user.Surname,
                Phone = user.PhoneNumber,
                Username = user.UserName,
                AvatarUrl = address + user.Avatar_Url
            };
            return result;
        }
    }
    #region классы-оболочки для генерации JSON ответов
    public class JsonRequestBody
    {
        public string Method { set; get; }
        public string Token { set; get; }
        public Dictionary<string,string> Parameters { set; get; }
    }
    
    public class JsonRespondBody
    {
        public string Status { set; get; }
        public object Result { set; get; }
        public string Error { set; get; }
    }

    public class UserSerealizerBody
    {
        public string Id { set; get; }
        public string Email { set; get; }
        public string Token { set; get; }
        public string Username { set; get; }
        public string FirstName { set; get; }
        public string Surname { set; get; }
        public string Phone { set; get; }
        public string AvatarUrl { set; get; }
        public string Address { set; get; }
        public string LastOnlineTime { set; get; }
        public string Description { set; get; }
    }
    public class DishSerealizerBody
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public string Ingridients { set; get; }
        public string Type { set; get; }
        public string Image { set; get; }
        public decimal Price { set; get; }
        public decimal? PriceWithIngridients { set; get; }
    }
    public class OfferSerealizerBody
    {
        public int Limit { set; get; }
        public int CurrentPage { set; get; }
        public IEnumerable<DishSerealizerBody> List { set; get; }
    }
    public class OrderSerializerBody
    {
        public int Id_Order { set; get; }
        public string Deadline { set; get; }
        public string IngridientsBuyer { set; get; }
        public string Payment { set; get; }
        public string Comunication { set; get; }
        public string Delivery { set; get; }
        public string Status { set; get; }
        public string Comment { set; get; }
        public decimal Total { set; get; }
        public string FinishTime { set; get; } 
        public UserSerealizerBody Cook { set; get; }
        public UserSerealizerBody Customer { set; get; }
        public IEnumerable<DishSerealizerBody> OrderList { set; get; }
    }
    #endregion
}
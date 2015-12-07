using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BistroDriveWebApp.Models
{
    public class DishRepository
    {
        BistroDriveEntities context;
        public DishRepository(BistroDriveEntities _context)
        {
            this.context = _context;
        }

        public dish GetDishById(int id)
        {
            return context.dishes.FirstOrDefault(d => d.Id_Dish == id);
        }

        public IEnumerable<dish> GetDishByUserId(string Id, int limit = 0)
        {
            IEnumerable<dish> result;
            if (limit == 0)
            {
                result = context.dishes.Where(d => d.Id_User == Id);
            }
            else
            {
                result = context.dishes.Where(d => d.Id_User == Id).Take(limit);
            }
            var dishtypes = GetDishTypes();
            foreach(var item in result)
            {
                item.dishtype = dishtypes.FirstOrDefault(dt => dt.Id_DishType == item.Id_Type);
            }
            return result;
        }

        public IEnumerable<dish> GetDishByTypeId(int Id)
        {
            return context.dishes.Where(d => d.Id_Type == Id);
        }

        public IEnumerable<dishtype> GetDishTypes()
        {
            return context.dishtypes.ToList();
        }

        public void AddDish(dish item)
        {
            context.dishes.Add(item);
            context.SaveChanges();
        }

        public string GetDishOwner(int Id_Dish)
        {
            dish item = GetDishById(Id_Dish);
            return item == null ? null : item.Id_User;
        }

        public void UpdateDish(DishViewModel model)
        {
            dish item = GetDishById(model.Id_Dish);
            /*if (item == null)
            {
                return;
            }*/
            item.Id_Type = model.Id_Type;
            item.Name = model.Name;
            item.Description = model.Description;
            item.ImageUrl = model.Image_Url;
            item.Price = model.Price;
            item.PriceWithIngridient = model.PriceWithIngridients;
            item.Ingridient = model.Ingridients;
            item.CanTeach = model.CanTeach != null;
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            dish d = GetDishById(id);
            context.dishes.Remove(d);
            context.SaveChanges();
        }

        public IEnumerable<dish> GetDishList()
        {
            var types = context.dishtypes.ToArray();
            var dishes = context.dishes.ToArray();
            var user = context.aspnetusers.ToArray();
            foreach(var item in dishes)
            {
                item.dishtype = types.FirstOrDefault(t=>t.Id_DishType == item.Id_Type);
                item.aspnetuser = user.FirstOrDefault(u => u.Id == item.Id_User);
            }
            return dishes;
        }

        public int GetDishCount()
        {
            return context.dishes.Count();
        }

        public IEnumerable<dish> GetDishList(int page, int pageSize, string search = "", int CityId = 0, 
            string dishinput = "", bool canTeach = false, bool travel = false, int minPrice = 0, int maxPrice = 0)
        {
            var types = context.dishtypes.ToArray();
            IEnumerable<dish> dishes = context.dishes.OrderByDescending(d=>d.Id_Dish).Skip(page*pageSize).Take(pageSize);
            if (search != "")
            {
                dishes = dishes.Where(d => d.Name.ToLower().IndexOf(search.ToLower()) != -1);
            }
            if(dishinput != "")
            {
                //преобразуем строку
                var dishesArr = dishinput.Split(';');
                dishes = dishes.Where(d => dishesArr.FirstOrDefault(n=> n == d.dishtype.Id_DishType.ToString()) != null);
            }
            if(canTeach == true)
            {
                dishes = dishes.Where(d => d.CanTeach == true);
            }
            if(travel == true)
            {
                dishes = dishes.Where(d => d.aspnetuser.withTravel == true);
            }
            if(minPrice != 0 && maxPrice != 0)
            {
                //работаем с int а цены в double. Для корректного сравнения
                minPrice--;
                maxPrice++;
                dishes = dishes.Where(d => (d.Price > minPrice && d.Price < maxPrice));
            }
            dishes = dishes.ToArray();
            var user = context.aspnetusers.ToArray();
            foreach (var item in dishes)
            {
                item.dishtype = types.FirstOrDefault(t => t.Id_DishType == item.Id_Type);
                item.aspnetuser = user.FirstOrDefault(u => u.Id == item.Id_User);
            }
            if (CityId != 0)
            {
                dishes = dishes.Where(d => d.aspnetuser.Id_City == CityId);
            }
            return dishes;
        }

        public int GetMaxPrice()
        {
            return Convert.ToInt32(context.dishes.Max(d => d.Price));
        }
        public int GetMinPrice()
        {
            return Convert.ToInt32(context.dishes.Min(d => d.Price));
        }
    }
}
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

        public IEnumerable<dish> GetDishList(int page, int pageSize)
        {
            var types = context.dishtypes.ToArray();
            var dishes = context.dishes.OrderByDescending(d=>d.Id_Dish).Skip(page*pageSize).Take(pageSize).ToArray();
            var user = context.aspnetusers.ToArray();
            foreach (var item in dishes)
            {
                item.dishtype = types.FirstOrDefault(t => t.Id_DishType == item.Id_Type);
                item.aspnetuser = user.FirstOrDefault(u => u.Id == item.Id_User);
            }
            return dishes;
        }
    }
}
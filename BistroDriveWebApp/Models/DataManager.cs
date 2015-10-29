using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BistroDriveWebApp.Models
{
    static class DataManager
    {
        static BistroDriveEntities _context;
        public static void Init()
        {
            _context = new BistroDriveEntities();
        }

        static UserRepository _user;
        public static UserRepository User
        {
            get
            {
                if(_user == null)
                {
                    _user = new UserRepository(_context);
                }
                return _user;
            }
        }

        static DishRepository _dish;
        public static DishRepository Dish
        {
            get
            {
                if (_dish == null)
                {
                    _dish = new DishRepository(_context);
                }
                return _dish;
            }
        }
    }
}
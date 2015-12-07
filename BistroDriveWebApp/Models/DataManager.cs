using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BistroDriveWebApp.Models
{
    public class DataManager
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

        static OrderRepository _order;
        public static OrderRepository Order
        {
            get
            {
                if (_order == null)
                {
                    _order = new OrderRepository(_context);
                }
                return _order;
            }
        }

        static DeliveryMethodRepository _deliveryMethod;
        public static DeliveryMethodRepository Delivery
        {
            get
            {
                if (_deliveryMethod == null)
                {
                    _deliveryMethod = new DeliveryMethodRepository(_context);
                }
                return _deliveryMethod;
            }
        }

        static PaymentMethodRepository _paymentMethod;
        public static PaymentMethodRepository PaymentMethod
        {
            get
            {
                if (_paymentMethod == null)
                {
                    _paymentMethod = new PaymentMethodRepository(_context);
                }
                return _paymentMethod;
            }
        }

        static StatusRepository _status;
        public static StatusRepository Status
        {
            get
            {
                if (_status == null)
                {
                    _status = new StatusRepository(_context);
                }
                return _status;
            }
        }

        static ContactMethodRepository _contact;
        public static ContactMethodRepository Contact
        {
            get
            {
                if (_contact == null)
                {
                    _contact = new ContactMethodRepository(_context);
                }
                return _contact;
            }
        }

        static IngridientsBuyerRepository _ingridientsBuyer;
        public static IngridientsBuyerRepository IngridientsBuyer
        {
            get
            {
                if (_ingridientsBuyer == null)
                {
                    _ingridientsBuyer = new IngridientsBuyerRepository(_context);
                }
                return _ingridientsBuyer;
            }
        }

        static GeolocationRepository _geolocation;
        public static GeolocationRepository Geolocation
        {
            get
            {
                if (_geolocation == null)
                {
                    _geolocation = new GeolocationRepository(_context);
                }
                return _geolocation;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BistroDriveWebApp.Models
{
    public class OrderRepository
    {
        BistroDriveEntities context;
        public OrderRepository(BistroDriveEntities _context)
        {
            this.context = _context;
        }


        public order GetOrderById(int id)
        {   
            var order = context.orders.FirstOrDefault(o => o.Id_Order == id);
            if(order == null)
            {
                return null;
            }
            order.ordercontactmethod = DataManager.Contact.GetContactById(order.Id_ContactMethod);
            order.orderdelivery = DataManager.Delivery.GetDeliveryById(order.Id_Delivery);
            order.orderingridientbuyer = DataManager.IngridientsBuyer.GetIngridientBuyerById(order.id_IngridientsBuyer);
            order.orderpaymentmethod = DataManager.PaymentMethod.GetPaymentMethodById(order.Id_PaymentMethod);
            order.orderstatu = DataManager.Status.GetOrderStatusById(order.Id_Status);
            order.aspnetuser = DataManager.User.GetUserById(order.Id_Cook);
            order.city = DataManager.Geolocation.GetCityById(order.id_city);
            return order;
        }

        public IEnumerable<order> GetOrdersByUserId(string id, bool incoming)
        {
            IEnumerable<order> result = null;
            if(incoming == true)
            {

                result = context.orders.Where(o => o.Id_Cook == id);
            }
            else
            {

                result = context.orders.Where(o => o.Id_Customer == id);
            }
            var contactmethod = context.ordercontactmethods.ToList();
            var delivery = context.orderdeliveries.ToList();
            var payment = context.orderpaymentmethods.ToList();
            var ingridientsbuyer = context.orderingridientbuyers.ToList();
            var status = context.orderstatus.ToList();
            var users = context.aspnetusers.ToList(); // этот костыль убрать!!!
            foreach(var item in result)
            {
                item.ordercontactmethod = contactmethod.FirstOrDefault(c=> c.Id_ContactMethod == item.Id_ContactMethod);
                item.orderdelivery = delivery.FirstOrDefault(d=>d.Id_Delivery == item.Id_Delivery);
                item.orderpaymentmethod = payment.FirstOrDefault(p=>p.Id_PaymentMethod == item.Id_PaymentMethod);
                item.orderingridientbuyer = ingridientsbuyer.FirstOrDefault(i=>i.Id_IngridientBuyer == item.id_IngridientsBuyer);
                item.orderstatu = status.FirstOrDefault(s => s.Id_Status == item.Id_Status);
                item.aspnetuser = users.FirstOrDefault(u => u.Id == item.Id_Cook);
            }
            return result;
        }

        public IEnumerable<orderproduct> GetOrderProducts(int id)
        {
            var dishes = context.dishes.ToList();
            var products =  context.orderproducts.Where(op => op.Id_Order == id);
            foreach(var item in products)
            {
                item.dish = dishes.FirstOrDefault(d => d.Id_Dish == item.Id_Dish);
            }
            return products;
        }

        public void AddOrder(order o, IEnumerable<orderproduct> product)
        {
            context.orders.Add(o);
            context.SaveChanges();
            decimal total = 0;
            //добовляем сгенерированый ID
            foreach (var item in product)
            {
                item.Id_Order = o.Id_Order;
                if (o.id_IngridientsBuyer == 1) // повар
                {
                    total += item.Price;
                }
                else
                {
                    total += item.PriceWithIngridients==null?0:(decimal)item.PriceWithIngridients;
                }
            }
            o.total = total;
            context.orderproducts.AddRange(product);
            context.SaveChanges();
        }

        public void UpdateStatus(int id_Order, int id_Status)
        {
            order o = GetOrderById(id_Order);
            o.Id_Status = id_Status;
            context.SaveChanges();
        }

        public void CloseOrder(int id_Order)
        {
            order o = DataManager.Order.GetOrderById(id_Order);
            o.Id_Status = 4; // завершённый
            o.FinishTime = DateTime.Now;
            context.SaveChanges();
        }
    }
}
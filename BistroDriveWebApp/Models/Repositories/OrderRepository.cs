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
            return context.orders.FirstOrDefault(o => o.Id_Order == id);
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
            foreach(var item in result)
            {
                item.ordercontactmethod = contactmethod.FirstOrDefault(c=> c.Id_ContactMethod == item.Id_ContactMethod);
                item.orderdelivery = delivery.FirstOrDefault(d=>d.Id_Delivery == item.Id_Delivery);
                item.orderpaymentmethod = payment.FirstOrDefault(p=>p.Id_PaymentMethod == item.Id_PaymentMethod);
                item.orderingridientbuyer = ingridientsbuyer.FirstOrDefault(i=>i.Id_IngridientBuyer == item.id_IngridientsBuyer);
            }
            return result;
        }

        public IEnumerable<orderproduct> GetOrderProducts(int id)
        {
            return context.orderproducts.Where(op => op.Id_Order == id);
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
    }
}
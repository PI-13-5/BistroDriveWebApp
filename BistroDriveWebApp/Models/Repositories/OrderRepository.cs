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

        public IEnumerable<order> GetOrdersByUserId(string id)
        {
            return context.orders.Where(o => (o.Id_Cook == id || o.Id_Customer == id));
        }
    }
}
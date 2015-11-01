using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BistroDriveWebApp.Models
{
    public class DeliveryMethodRepository
    {
        BistroDriveEntities context;
        public DeliveryMethodRepository(BistroDriveEntities _context)
        {
            this.context = _context;
        }
        public IEnumerable<orderdelivery> GetDeliveryMethods()
        {
            return context.orderdeliveries.ToList();
        }

        public orderdelivery GetDeliveryById(int id)
        {
            return context.orderdeliveries.FirstOrDefault(d => d.Id_Delivery == id);
        }
    }
}
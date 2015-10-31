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
        public IEnumerable<orderdelivery> GetContactMethods()
        {
            return context.orderdeliveries.ToList();
        }
    }
}
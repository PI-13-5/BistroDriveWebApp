using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BistroDriveWebApp.Models
{
    public class IngridientsBuyerRepository
    {

        BistroDriveEntities context;
        public IngridientsBuyerRepository(BistroDriveEntities _context)
        {
            this.context = _context;
        }
        public IEnumerable<orderingridientbuyer> GetIngridientsBuyers()
        {
            return context.orderingridientbuyers.ToList();
        }
        
        public orderingridientbuyer GetIngridientBuyerById(int id)
        {
            return context.orderingridientbuyers.FirstOrDefault(o => o.Id_IngridientBuyer == id);
        }
    }
    
}
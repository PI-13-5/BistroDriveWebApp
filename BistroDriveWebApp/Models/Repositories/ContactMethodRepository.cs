using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BistroDriveWebApp.Models
{
    public class ContactMethodRepository
    {
        BistroDriveEntities context;
        public ContactMethodRepository(BistroDriveEntities _context)
        {
            this.context = _context;
        }
        public IEnumerable<ordercontactmethod> GetContactMethods()
        {
            return context.ordercontactmethods.ToList();
        }

        public ordercontactmethod GetContactById(int id)
        {
            return context.ordercontactmethods.FirstOrDefault(c => c.Id_ContactMethod == id);
        }
    }
}
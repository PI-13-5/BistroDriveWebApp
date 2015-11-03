using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BistroDriveWebApp.Models
{
    public class PaymentMethodRepository
    {
        BistroDriveEntities context;
        public PaymentMethodRepository(BistroDriveEntities _context)
        {
            this.context = _context;
        }

        public IEnumerable<orderpaymentmethod> GetPaymentMethods()
        {
            return context.orderpaymentmethods.ToList();
        }

        public orderpaymentmethod GetPaymentMethodById(int id)
        {
            return context.orderpaymentmethods.FirstOrDefault(p => p.Id_PaymentMethod == id);
        }
    }
}
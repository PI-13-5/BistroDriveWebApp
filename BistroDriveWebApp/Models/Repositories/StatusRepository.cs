﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BistroDriveWebApp.Models
{
    public class StatusRepository
    {
        BistroDriveEntities context;
        public StatusRepository(BistroDriveEntities _context)
        {
            this.context = _context;
        }

        public IEnumerable<orderstatu> GetOrderStatuses()
        {
            return context.orderstatus.ToList();
        }

        public orderstatu GetOrderStatusById(int id)
        {
            return context.orderstatus.FirstOrDefault(o => o.Id_Status == id);
        }
    }
}
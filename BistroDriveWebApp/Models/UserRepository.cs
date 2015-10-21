using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BistroDriveWebApp.Models
{
    public class UserRepository
    {
        BistroDriveEntities context;
        public UserRepository(BistroDriveEntities _context)
        {
            this.context = _context;
        }

        public aspnetuser GetUserById(string id)
        {
            return context.aspnetusers.SingleOrDefault(u => u.Id == id);
        }

        public IEnumerable<aspnetuser> GetUsers()
        {
            return context.aspnetusers.ToList();
        }
    }
}
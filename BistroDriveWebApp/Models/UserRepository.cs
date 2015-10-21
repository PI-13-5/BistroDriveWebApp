using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BistroDriveWebApp.Models
{
    public class UserRepository
    {
        DataBaseEntities context;
        public UserRepository(DataBaseEntities _context)
        {
            this.context = _context;
        }

        public AspNetUser GetUserById(string id)
        {
            return context.AspNetUsers.SingleOrDefault(u => u.Id == id);
        }

        public IEnumerable<AspNetUser> GetUsers()
        {
            return context.AspNetUsers.ToList();
        }
    }
}
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

        public void UpdateUserAvatar(string id, string Url)
        {
            var user = GetUserById(id);
            user.Avatar_Url = Url;
            context.SaveChanges();
        }

        public string GetUserAvatar(string id)
        {
            return GetUserById(id).Avatar_Url;
        }

        public aspnetuser GetUserByName(string userName)
        {
            return context.aspnetusers.FirstOrDefault(n => n.UserName == userName);
        }

        public string GetUserDescription(string id)
        {
            var result =  context.userdescriptions.FirstOrDefault(u => u.Id_User == id);
            if(result == null)
            {
                return null;
            }
            else
            {
                return result.Description;
            }
        }
        public userdescription FindUserDescription(string id)
        {
            return context.userdescriptions.FirstOrDefault(u => u.Id_User == id);
        }

        public void UpdateUserDescription(string id, string description)
        {
            var old = FindUserDescription(id);
            if (old == null)
            {
                userdescription item = new userdescription { Id_User = id, Description = description };
                context.userdescriptions.Add(item);
            }
            else
            {
                old.Description = description;
            }
            context.SaveChanges();
        }

        public void UpdateUser(string id, ProfileSettingsViewModel model)
        {
            var user = GetUserById(id);
            user.FristName = model.FirstName;
            user.Surname = model.Surname;
            user.PhoneNumber = model.Telphone;
            user.Address = model.Address;
            context.SaveChanges();
            UpdateUserDescription(id, model.Description);
        }
    }
}
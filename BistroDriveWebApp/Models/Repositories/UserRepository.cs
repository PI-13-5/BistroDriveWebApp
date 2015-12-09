using BistroDriveWebApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BistroDriveWebApp.Models
{
    public class UserRepository
    {
        BistroDriveEntities context;
        List<aspnetuser> buffer;
        List<userdescription> desbuffer;
        public UserRepository(BistroDriveEntities _context)
        {
            this.context = _context;
            buffer = _context.aspnetusers.ToList();
            desbuffer = _context.userdescriptions.ToList();
        }
        public void RefreshBuffer()
        {
            buffer = context.aspnetusers.ToList();
            desbuffer = context.userdescriptions.ToList();
        }
        public aspnetuser GetUserByIdBuffered(string id)
        {
            return buffer.SingleOrDefault(u => u.Id == id);
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
            var result = context.userdescriptions.FirstOrDefault(u => u.Id_User == id);
            if (result == null)
            {
                return null;
            }
            else
            {
                return result.Description;
            }
        }

        public string GetUserDescriptionBuffered(string id)
        {
            var result = desbuffer.FirstOrDefault(u => u.Id_User == id);
            if (result == null)
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

        public usertoken GetTokenById(int id)
        {
            return context.usertokens.FirstOrDefault(t => t.Id_Token == id);
        }

        public usertoken GetTokenByToken(string token)
        {
            //добавить проверку на время и удаление
            return context.usertokens.FirstOrDefault(t => t.Token == token);
        }

        public usertoken GenerateToken(string id_user)
        {
            var rtoken = Security.GetMd5(id_user + DateTime.Now.ToFileTimeUtc());
            var token = new usertoken
            {
                Id_User = id_user,
                Expiration = DateTime.Now.AddDays(30),
                Token = rtoken
            };
            context.usertokens.Add(token);
            context.SaveChanges();
            return token;
        }

        public aspnetuser GetUserByToken(string id)
        {
            if(id== null)
            {
                return null;
            }
            var token = GetTokenByToken(id);
            if (token == null)
            {
                return null;
            }
            return context.aspnetusers.FirstOrDefault(u => u.Id == token.Id_User);
        }

        public void DeleteAllUserToken(string id_user)
        {
            context.usertokens.RemoveRange(context.usertokens.Where(t => t.Id_User == id_user));
            context.SaveChanges();
        }

        public void UpdateUser(string id, ProfileSettingsViewModel model)
        {
            var user = GetUserById(id);
            user.FristName = model.FirstName;
            user.Surname = model.Surname;
            user.PhoneNumber = model.Telphone;
            user.Address = model.Address;
            user.Id_City = model.City;
            user.withTravel = model.WithTravel != null;
            context.SaveChanges();
            UpdateUserDescription(id, model.Description);
        }

        public IEnumerable<aspnetuser> GetAllUser()
        {
            return context.aspnetusers;
        }

        public IEnumerable<aspnetuser> GetAllUser(int page, int pageSize, ref int usercount, string search = "", int CityId = 0, bool travel = false)
        {
            IEnumerable<aspnetuser> users = context.aspnetusers.OrderByDescending(u=>u.Id);
            if (search != "")
            {
                users = users.Where(d => (
                (d.FristName+" "+d.Surname).ToLower().IndexOf(search.ToLower()) != -1 ||
                d.UserName.ToLower().IndexOf(search.ToLower()) != -1
                ));
            }
            if (travel == true)
            {
                users = users.Where(u => u.withTravel == true);
            }
            users = users.ToArray();
            if (CityId != 0)
            {
                users = users.Where(d => d.Id_City == CityId);
            }
            usercount = users.Count();
            users = users.Skip(page * pageSize).Take(pageSize);
            var descr = context.userdescriptions.ToArray();
            var cities = context.cities.ToList();
            foreach (var item in users)
            {
                item.userdescriptions.Add(descr.FirstOrDefault(d => d.Id_User == item.Id));
                item.city = cities.FirstOrDefault(c => c.id_city == item.Id_City);
            }
            return users;
        }

        public void AddUserReview(string id_user, string owner, int mark, string text)
        {
            review r = new review
            {
                Id_Owner = owner,
                Id_User = id_user,
                Mark = mark,
                Text = text
            };
            context.reviews.Add(r);
            context.SaveChanges();
            UpdateUserRaiting(r.Id_User);
        }

        private void UpdateUserRaiting(string Id_User)
        {
            var user = context.reviews.Where(rev => rev.Id_User == Id_User);
            aspnetuser d = GetUserById(Id_User);
            if (user.Count() != 0)
            {
                double average = (double)user.Average(rev => rev.Mark);
                d.Raiting = Convert.ToInt32(Math.Round(average, 0));
            }
            else
            {
                d.Raiting = 0;
            }
            context.SaveChanges();

        }

        public review GetReviewById(int id)
        {
            return context.reviews.FirstOrDefault(r => r.Id_UserReview == id);
        }

        public void EditUserReview(int id, int mark, string text)
        {
            review r = GetReviewById(id);
            r.Mark = mark;
            r.Text = text;
            context.SaveChanges();
            UpdateUserRaiting(r.Id_User);
        }

        public void DeleteUserReview(int id)
        {
            review r = GetReviewById(id);
            string user_id = r.Id_User;
            context.reviews.Remove(r);
            context.SaveChanges();
            UpdateUserRaiting(user_id);
        }
        
        public List<review> GetReviewByUserId(string user_id)
        {
            return context.reviews.Where(r => r.Id_User == user_id).ToList();
        }
    }
}
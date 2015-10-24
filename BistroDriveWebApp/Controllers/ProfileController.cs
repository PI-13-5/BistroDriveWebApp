using BistroDriveWebApp.Core;
using BistroDriveWebApp.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BistroDriveWebApp.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var user = DataManager.User.GetUserById(userId);
            ViewBag.UserName = user.UserName;
            ViewBag.AvatarUrl = user.Avatar_Url;
            ViewBag.FristName = user.FristName;
            ViewBag.Surname = user.Surname;
            ViewBag.Description = DataManager.User.GetUserDescription(userId) ;
            return View();
        }

        
    }
}
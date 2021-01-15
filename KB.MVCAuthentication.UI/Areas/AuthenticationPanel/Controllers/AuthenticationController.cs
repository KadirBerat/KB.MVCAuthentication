using KB.MVCAuthentication.UI.Areas.AuthenticationPanel.Models.ViewModels;
using KB.MVCAuthentication.UI.Helpers.Authentication;
using KB.MVCAuthentication.UI.Models;
using KB.MVCAuthentication.UI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KB.MVCAuthentication.UI.Areas.AuthenticationPanel.Controllers
{
    public class AuthenticationController : Controller
    {
        private MVCAuthenticationContext db = new MVCAuthenticationContext();

        // GET: AuthenticationPanel/Authentication
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (!String.IsNullOrEmpty(model.Username) && !String.IsNullOrEmpty(model.Password))
            {
                var user = db.Users.FirstOrDefault(s => (s.Email == model.Username || s.Username == model.Username) && s.Password == model.Password && s.IsActive == true);

                //Login with cookie start
                UserAuthentication.LoginServiceWithCookie(user.UserId, user.Username, user.Role);
                if (user.Role == 0)
                    return RedirectToAction("Index", "UserHome", new { area = "UserPanel" });
                else if (user.Role == 1 || user.Role == 2)
                    return RedirectToAction("Index", "UserHome", new { area = "ManagementPanel" });
                //Login with cookie end

                //Login with session start
                AuthenticationSessionViewModel _SessionData = UserAuthentication.LoginServiceWithSession(user.UserId, user.Username, user.Role);
                Session.Add("userauthsession", _SessionData);
                if (user.Role == 0)
                    return RedirectToAction("Index", "UserHome", new { area = "UserPanel" });
                else if (user.Role == 1 || user.Role == 2)
                    return RedirectToAction("Index", "UserHome", new { area = "ManagementPanel" });
                //Login with session end


            }
            return View();
        }
    }
}
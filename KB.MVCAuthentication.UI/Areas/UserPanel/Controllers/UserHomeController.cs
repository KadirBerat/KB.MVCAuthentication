using KB.MVCAuthentication.UI.Helpers.Authentication.AuthFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KB.MVCAuthentication.UI.Areas.UserPanel.Controllers
{
    public class UserHomeController : Controller
    {
        [UserAuth] //Authentication filtresi
        public ActionResult Index()
        {
            return View();
        }
    }
}
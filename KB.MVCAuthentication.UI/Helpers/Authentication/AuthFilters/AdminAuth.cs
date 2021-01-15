using KB.MVCAuthentication.UI.Helpers.Security;
using KB.MVCAuthentication.UI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace KB.MVCAuthentication.UI.Helpers.Authentication.AuthFilters
{
    public class AdminAuth : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            //Cookie
            if (HttpContext.Current.Request.Cookies["userauthcookie"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary { { "area", "AuthenticationPanel" }, { "controller", "Authentication" }, { "action", "Login" } });
            }
            else
            {
                string hashTicket = HttpContext.Current.Request.Cookies["userauthcookie"].Value;
                FormsAuthenticationTicket cleanTicket = FormsAuthentication.Decrypt(hashTicket);
                if (cleanTicket.Version != 1 || cleanTicket.Version != 2)
                    filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary { { "area", "AuthenticationPanel" }, { "controller", "Authentication" }, { "action", "Login" } });
            }
            //

            //session
            if (HttpContext.Current.Session["userauthsession"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary { { "area", "AuthenticationPanel" }, { "controller", "Authentication" }, { "action", "Login" } });
            }
            else
            {
                AuthenticationSessionViewModel _EncryptedData = (AuthenticationSessionViewModel)HttpContext.Current.Session["userauthsession"];
                TripleDESHelper tdh = new TripleDESHelper();
                byte _Role = Convert.ToByte(tdh.TripleDESDecrypt(_EncryptedData.SessionDatas.RoleID));
                if (_Role != 1 || _Role != 2)
                    filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary { { "area", "AuthenticationPanel" }, { "controller", "Authentication" }, { "action", "Login" } });
            }
            //
        }
    }
}
using KB.MVCAuthentication.UI.Helpers.Security;
using KB.MVCAuthentication.UI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace KB.MVCAuthentication.UI.Helpers.Authentication
{
    public class UserAuthentication
    {
        private static void LoginWithCookie(byte role, string username, string userData)
        {
            DateTime expire = DateTime.Now.AddDays(1);
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(role, username, DateTime.Now, expire, false, userData);
            string hashTicket = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie("userauthcookie", hashTicket);
        }

        //0 => User
        //1 => Admin
        //2 => MasterAdmin
        public static void LoginServiceWithCookie(short UserID, string username, byte role)
        {
            string _Role = "";
            if (role == 0)
                _Role = "User";
            else if (role == 1)
                _Role = "Admin";
            else if (role == 2)
                _Role = "MasterAdmin";

            string _UserData = UserID.ToString() + "-" + username + "-" + _Role;

            LoginWithCookie(role, username, _UserData);
        }

        //0 => User
        //1 => Admin
        //2 => MasterAdmin
        public static AuthenticationSessionViewModel LoginServiceWithSession(short UserID, string username, byte role)
        {
            string _Role = "";
            if (role == 0)
                _Role = "User";
            else if (role == 1)
                _Role = "Admin";
            else if (role == 2)
                _Role = "MasterAdmin";

            string _UserData = role.ToString() + "-" + UserID.ToString() + "-" + username + "-" + _Role;

            TripleDESHelper tdh = new TripleDESHelper();
            AuthenticationSessionViewModel _Data = new AuthenticationSessionViewModel();
            _Data.SessionDatas.UserID = tdh.TripleDESEncrypt(UserID.ToString());
            _Data.SessionDatas.username = tdh.TripleDESEncrypt(username.ToString());
            _Data.SessionDatas.RoleID = tdh.TripleDESEncrypt(role.ToString());
            _Data.SessionDatas.Role = tdh.TripleDESEncrypt(_Role.ToString());
            return _Data;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KB.MVCAuthentication.UI.Models.ViewModels
{
    public class AuthenticationSessionViewModel
    {
        public SessionData SessionDatas { get; set; }
        public class SessionData
        {
            public string RoleID { get; set; }
            public string UserID { get; set; }
            public string username { get; set; }
            public string Role { get; set; }
        }
    }
}
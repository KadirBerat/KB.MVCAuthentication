using System.Web.Mvc;

namespace KB.MVCAuthentication.UI.Areas.AuthenticationPanel
{
    public class AuthenticationPanelAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AuthenticationPanel";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AuthenticationPanel_default",
                "AuthenticationPanel/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MB.AgilePortfolio.MVCUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                name: "AdminUserList",
                url: "User/",
                defaults: new { controller = "User", action = "Index" }
            );

            routes.MapRoute(
                name: "AdminUserCreate",
                url: "User/Create",
                defaults: new { controller = "User", action = "Create" }
            );


            routes.MapRoute(
                name: "PublicProfile",
                url: "User/{username}",
                defaults: new { controller = "UserProfile", action = "PublicProfile", username = UrlParameter.Optional }
            );



            routes.MapRoute(
                name: "ResetPassword",
                url: "ResetPassword/{passwordResetId}",
                defaults: new { controller = "Login", action = "ResetPassword", passwordResetId = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "About",
                url: "About",
                defaults: new { controller = "Home", action = "About"}
            );



            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

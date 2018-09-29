using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //  route mở rộng
            routes.MapMvcAttributeRoutes();  
            // custome
            routes.MapRoute(
                name:"About",
                url:"Trang-Chu/Gioi-Thieu-Inox-Thaibinh",
                defaults: new { controller = "Home", action = "About", id = UrlParameter.Optional }
                );
            routes.MapRoute(
                name: "Index2",
                url: "Trang-Chu/Inox-ThaiBinh",
                defaults: new { controller = "Home", action = "Index2", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Category",
                url: "Trang-Chu/San-Pham-Inox/{id}",
                defaults: new { controller = "Home", action = "Category", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Customer",
                url: "Trang-Chu/Du-An-Inox",
                defaults: new { controller = "Home", action = "Customer", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Contact",
                url: "Trang-Chu/Lien-He-Inox-ThaiBinh",
                defaults: new { controller = "Home", action = "Contact", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Product",
                url: "Trang-Chu/Chi-Tiet-San-Pham-Inox/{id}",
                defaults: new { controller = "Home", action = "Product", id = UrlParameter.Optional }
            );
            // route mặc định
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index2", id = UrlParameter.Optional }
            );
        }
    }
}

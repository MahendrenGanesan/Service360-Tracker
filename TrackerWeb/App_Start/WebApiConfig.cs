using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace TrackerWeb
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
               name: "DefaultGetByIdApi",
               routeTemplate: "api/{controller}/GetById/{id}",
               defaults: new { action = "GetById" },
               constraints: new { id = @"^\d+$" } // id must be digits
            );
            config.Routes.MapHttpRoute(
              name: "DefaultGetByMailApi",
              routeTemplate: "api/{controller}/GetByMail/{email}",
              defaults: new { action = "GetByMail" } 
           );
        }
    }
}

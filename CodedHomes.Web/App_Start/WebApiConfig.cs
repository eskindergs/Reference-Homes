using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CodedHomes.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
               name: "API",
               routeTemplate: "api/homes/{id}",
               defaults: new
               {
                   controller = "HomesAPI",
                   id = RouteParameter.Optional
               }
           );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}

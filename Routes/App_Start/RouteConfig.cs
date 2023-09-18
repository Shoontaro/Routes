using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Routes
{
    public class RouteConfig
    {

        //новый класс ограничения

        public class CustomConstraint : IRouteConstraint
        {
            private string uri;

            public CustomConstraint(string uri)
            {
                this.uri = uri;
            }

            public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
            {
                return !(uri == httpContext.Request.Url.AbsolutePath);
                //throw new NotImplementedException();
            }
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}"); //игнорирует. можно использовать и для игнореирования любых других путей

            routes.IgnoreRoute("/Home/Index/12");//отклонение запроса

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }, 
                constraints: new { /*controller = "^H.*",*/ id = @"\d{2}", //ограничения маршрутов (должно осоответствовать регулярному выражению, id должно иметь значение из двух цифр)

                    myConstraint = new CustomConstraint("/Home/Index/12") } //на данную строку запросов накладывается ограничение
                    /*httpMethod = new HttpMethodConstraint("Get")}*/ //только те маршруты, которые имеют тип Гет
            );
        }
    }
}

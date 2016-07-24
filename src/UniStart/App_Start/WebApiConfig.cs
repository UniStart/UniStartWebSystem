namespace UniStart
{
    using System.Web.Http;

    public static class WebApiConfig
    {
        public static void MapHttpRoutes(this HttpRouteCollection routes)
        {
            routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "api/{controller}/{action}/{id}",
                    defaults: new { id = RouteParameter.Optional });
        }
    }
}
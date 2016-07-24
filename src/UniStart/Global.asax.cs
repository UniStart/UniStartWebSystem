namespace UniStart
{
    using System.Web.Http;
    using Autofac.Integration.WebApi;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using Autofac.Integration.Mvc;

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            
            GlobalConfiguration.Configure(configuration =>
            {
                configuration.Routes.MapHttpRoutes();
                configuration.MapHttpAttributeRoutes();
            });

            RouteConfig.RegisterRoutes(RouteTable.Routes);

            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
            var container = DependencyInjection.DependencyInjection.RegisterContainer();

            // Set the dependency resolver for Web API.
            var webApiResolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = webApiResolver;

            // Set the dependency resolver for MVC.
            var mvcResolver = new AutofacDependencyResolver(container);
            DependencyResolver.SetResolver(mvcResolver);

            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            json.UseDataContractJsonSerializer = true;
        }
    }
}

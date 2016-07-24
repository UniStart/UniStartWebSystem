using System.Reflection;
using System.Web.Http;
using Autofac.Integration.WebApi;

namespace UniStart.DependencyInjection
{
    using System;
    using Autofac;
    using Unistart.Models;
    using UniStart.Data;
    using UniStart.Data.Repositories;
    using Autofac.Integration.Mvc;
    using UniStart.Controllers;

    public class DependencyInjection
    {
        public static IContainer RegisterContainer(Action<ContainerBuilder> bindingOverride = null)
        {
            var builder = new ContainerBuilder();
            
            // Data
            builder.RegisterType<UniStartContext>().As<IUniStartDbContext>();
            builder.RegisterType<Repository<Lecture>>().As<IRepository<Lecture>>();
            builder.RegisterType<Repository<Topic>>().As<IRepository<Topic>>();

            // Controllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterControllers(typeof(LectureController).Assembly);
            builder.RegisterControllers(typeof(PingController).Assembly);

            return builder.Build();
        }
    }
}

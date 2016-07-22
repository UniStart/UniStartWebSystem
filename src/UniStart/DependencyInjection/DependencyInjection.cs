using Autofac.Integration.Mvc;
using UniStart.Controllers;

namespace UniStart.DependencyInjection
{
    using System;
    using Autofac;
    using Unistart.Models;
    using UniStart.Data;
    using UniStart.Data.Repositories;

    public class DependencyInjection
    {
        public static IContainer Register(Action<ContainerBuilder> bindingOverride = null)
        {
            var builder = new ContainerBuilder();
            
            // Data
            builder.RegisterType<UniStartContext>().As<IUniStartDbContext>();
            builder.RegisterType<Repository<Lecture>>().As<IRepository<Lecture>>();
            builder.RegisterType<Repository<Topic>>().As<IRepository<Topic>>();

            // Controllers
            builder.RegisterControllers(typeof(LectureController).Assembly);

            return builder.Build();
        }
    }
}
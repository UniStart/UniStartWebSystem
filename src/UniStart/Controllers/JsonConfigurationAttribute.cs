namespace UniStart.Controllers
{
    using System;
    using System.Net.Http.Formatting;
    using System.Web.Http.Controllers;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class JsonConfigurationAttribute : Attribute, IControllerConfiguration
    {
        public void Initialize(HttpControllerSettings controllerSettings, HttpControllerDescriptor controllerDescriptor)
        {
            controllerSettings.Formatters.Clear();
            controllerSettings.Formatters.Add(new JsonMediaTypeFormatter());
        }
    }
}

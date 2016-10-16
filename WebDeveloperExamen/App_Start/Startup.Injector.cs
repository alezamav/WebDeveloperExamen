using LightInject;
using System;
using System.Reflection;

namespace WebDeveloperExamen
{
    public partial class Startup
    {
        public void ConfigInjector()
         {
            var container = new ServiceContainer();
            container.RegisterAssembly(Assembly.GetExecutingAssembly());
            container.RegisterAssembly("WebDeveloperExamen.Model*.dll");
            container.RegisterAssembly("WebDeveloperExamen.Repository*.dll");
            container.RegisterControllers();
            container.EnableMvc();
        }


    }
}
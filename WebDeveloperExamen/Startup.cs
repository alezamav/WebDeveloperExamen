using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebDeveloperExamen.Startup))]
namespace WebDeveloperExamen
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
           ConfigInjector();
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ChurchWebApp_Web.Startup))]
namespace ChurchWebApp_Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

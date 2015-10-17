using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BistroDriveWebApp.Startup))]
namespace BistroDriveWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

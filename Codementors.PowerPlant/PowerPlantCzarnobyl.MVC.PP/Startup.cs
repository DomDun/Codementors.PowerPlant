using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PowerPlantCzarnobyl.MVC.PP.Startup))]
namespace PowerPlantCzarnobyl.MVC.PP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

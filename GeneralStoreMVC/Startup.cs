using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GeneralStoreMVC.Startup))]
namespace GeneralStoreMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

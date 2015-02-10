using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Connecto.App.Startup))]
namespace Connecto.App
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

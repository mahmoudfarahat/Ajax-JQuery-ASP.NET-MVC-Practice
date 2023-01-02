using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(dashboard.Startup))]
namespace dashboard
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

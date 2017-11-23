using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EX11.Startup))]
namespace EX11
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

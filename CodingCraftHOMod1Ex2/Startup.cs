using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EX2.Startup))]
namespace EX2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

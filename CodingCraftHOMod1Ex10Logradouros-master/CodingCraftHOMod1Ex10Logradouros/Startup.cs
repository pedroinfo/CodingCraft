using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CodingCraftHOMod1Ex10Logradouros.Startup))]
namespace CodingCraftHOMod1Ex10Logradouros
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

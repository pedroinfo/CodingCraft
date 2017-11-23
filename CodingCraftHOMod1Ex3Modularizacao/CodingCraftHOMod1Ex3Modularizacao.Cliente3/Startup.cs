using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CodingCraftHOMod1Ex3Modularizacao.Cliente3.Startup))]
namespace CodingCraftHOMod1Ex3Modularizacao.Cliente3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

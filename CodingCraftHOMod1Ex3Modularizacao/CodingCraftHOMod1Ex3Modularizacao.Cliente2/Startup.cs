using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CodingCraftHOMod1Ex3Modularizacao.Cliente2.Startup))]
namespace CodingCraftHOMod1Ex3Modularizacao.Cliente2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

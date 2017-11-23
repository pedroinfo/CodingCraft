using Owin;

namespace CodingCraftHOMod1Ex4Identity.Samples
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

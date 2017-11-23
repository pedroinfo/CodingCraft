using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(CodingCraftHOMod1Ex3Modularizacao.Api.Startup))]

namespace CodingCraftHOMod1Ex3Modularizacao.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

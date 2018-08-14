using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using NuGet.Server;
using NuGet.Server.V2;
using System.Web.Mvc;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(CodingCraftHOMod1Ex8NuGet.App_Start.NuGetODataConfig), "Start")]

namespace CodingCraftHOMod1Ex8NuGet.App_Start 
{
    public static class NuGetODataConfig 
	{
        public static void Start() 
		{
            ServiceResolver.SetServiceResolver(new DefaultServiceResolver());

            var config = GlobalConfiguration.Configuration;

            NuGetV2WebApiEnabler.UseNuGetV2WebApiFeed(config, "NuGetDefault", "nuget", "PackagesOData");

        
            


            config.Routes.MapHttpRoute(
                name: "NuGetDefault_ClearCache",
                routeTemplate: "nuget/clear-cache",
                defaults: new { controller = "PackagesOData", action = "ClearCache" },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) }
            );



        }
    }
}

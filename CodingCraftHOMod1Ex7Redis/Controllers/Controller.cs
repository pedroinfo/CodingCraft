using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Newtonsoft;
using System.Web.Routing;

namespace CodingCraftHOMod1Ex7Redis.Controllers
{
    public abstract class Controller : System.Web.Mvc.Controller
    {
        private NewtonsoftSerializer serializer = new NewtonsoftSerializer();
        public StackExchangeRedisCacheClient RedisCacheClient { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            var configuration = new ConfigurationOptions();
            configuration.EndPoints.Add("localhost:6379");
            configuration.ConnectTimeout = 200000;
            configuration.SyncTimeout = 200000;
            RedisCacheClient = new StackExchangeRedisCacheClient(ConnectionMultiplexer.Connect(configuration), serializer);
            
        }
    }
}
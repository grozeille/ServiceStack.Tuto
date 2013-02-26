using System.Net;
using Funq;
using ServiceStack.DataAnnotations;
using ServiceStack.Logging;
using ServiceStack.Logging.Support.Logging;
using ServiceStack.OrmLite;
using ServiceStack.Razor;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.Text;
using ServiceStack.WebHost.Endpoints;
using ServiceStack.Tuto.WebHost.Repositories;
using PricerTuto;

//The entire C# code for the stand-alone RazorRockstars demo.
namespace ServiceStack.Tuto.WebHost
{
    public class AppHost : AppHostBase
    {
        public AppHost() : base("Test Razor", typeof(AppHost).Assembly) { }

        public override void Configure(Container container)
        {
            LogManager.LogFactory = new ServiceStack.Logging.Log4Net.Log4NetFactory(true);

            LogManager.GetLogger(typeof(AppHost)).Debug("Start");

            //container.Register<IQuoteRepository>(new QuoteRepository());
            container.Register<IQuoteRepository>(new MongoDBQuoteRepository());
            container.Register<IDateService>(new DefaultDateService());
            container.Register<IMarketDataService>(new GoogleMarketDataService());

            Plugins.Add(new RazorFormat());

            SetConfig(new EndpointHostConfig {
                CustomHttpHandlers = {
                    { HttpStatusCode.NotFound, new RazorHandler("/notfound") },
                }
            });
        }
   }
}
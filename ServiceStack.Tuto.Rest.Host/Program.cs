using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.WebHost.Endpoints;
using ServiceStack.Tuto.Rest.Common.Services;
using Funq;
using ServiceStack.Tuto.Rest.Common.Repositories;
using ServiceStack.ServiceInterface.Admin;

namespace ServiceStack.Tuto.Rest.Host
{
    class Program
    {
        public class AppHost : AppHostHttpListenerBase
        {
            public AppHost() : base("REST", typeof(QuoteService).Assembly) { }

            public override void Configure(Container container)
            {
                container.Register<QuoteRepository>(new QuoteRepository());                
            }
        }

        static void Main(string[] args)
        {
            var listeningOn = args.Length == 0 ? "http://*:50903/" : args[0];
            var appHost = new AppHost();
            appHost.Init();
            appHost.Start(listeningOn);

            Console.WriteLine("AppHost Created at {0}, listening on {1}", DateTime.Now, listeningOn);
            Console.ReadKey();
        }
    }
}

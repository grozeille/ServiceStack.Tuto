using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PricerTuto;
using ServiceStack.Tuto.WebHost.Repositories;
using ServiceStack.ServiceHost;

namespace ServiceStack.Tuto.WebHost.Services
{
    [Route("/delta")]
    public class Delta
    {
        public string Symbol { get; set; }

        public double Spot { get; set; }

        public double Volatility { get; set; }

        public double Rate { get; set; }

        public double Strike { get; set; }

        public DateTime SelectedMaturity { get; set; }

        public OptionType OptionType { get; set; }
    }

    public class DeltaDetails
    {
        public double name { get; set; }

        public double y { get; set; }
    }

    public class DeltaResponse
    {
        public DeltaDetails[] Delta { get; set; }

        public DeltaDetails[] Gamma { get; set; }

        public DeltaDetails[] Payoff { get; set; }

        public DeltaDetails[] Price { get; set; }
    }

    public class DeltaService : ServiceStack.ServiceInterface.Service
    {
        public IDateService DateService { get; set; }

        public IQuoteRepository QuoteRepository { get; set; }

        public object Post(Delta request)
        {
            var pricer = new Pricer { DateService = this.DateService, MarketDataService = new QuoteMarketDataService { QuoteRepository = QuoteRepository } };

            var variation = 10;

            double[] spotScenarios = new double[11];
            spotScenarios[5] = request.Spot;
            for (int i = 1; i <= 5; i++)
            {
                spotScenarios[5 + i] = request.Spot + ((variation * i) / 100.0) * request.Spot;
            }
            for (int i = 1; i <= 5; i++)
            {
                spotScenarios[5 - i] = request.Spot - ((variation * i) / 100.0) * request.Spot;
            }

            var delta = new List<DeltaDetails>();
            var gamma = new List<DeltaDetails>();
            var price = new List<DeltaDetails>();
            var payoff = new List<DeltaDetails>();

            int cptScenario = 0;
            foreach (double spotScenario in spotScenarios)
            {
                var optionScenario = pricer.Price(
                    request.OptionType,
                    request.Symbol,
                    request.SelectedMaturity,
                    request.Strike,
                    spotScenario,
                    request.Volatility,
                    request.Rate);

                price.Add(new DeltaDetails{ name = spotScenario, y = optionScenario.Price});
                payoff.Add(new DeltaDetails{ name = spotScenario, y = optionScenario.PayOff});
                delta.Add(new DeltaDetails{ name = spotScenario, y = optionScenario.Delta});
                gamma.Add(new DeltaDetails { name = spotScenario, y = optionScenario.Gamma });
                cptScenario++;
            }

            return new DeltaResponse { Delta = delta.ToArray(), Gamma = gamma.ToArray(), Payoff = payoff.ToArray(), Price = price.ToArray() };
        }
    }
}
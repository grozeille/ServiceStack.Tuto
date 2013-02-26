using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;
using ServiceStack.Tuto.WebHost.Model;
using PricerTuto;
using ServiceStack.Tuto.WebHost.Repositories;
using ServiceStack.ServiceInterface;

namespace ServiceStack.Tuto.WebHost.Services
{
    [Route("/price", Verbs="GET")]
    public class PricerPage
    {
        public string Symbol { get; set; }
    }

    [Route("/price", Verbs="POST")]
    public class Price
    {
        public string Symbol { get; set; }

        public double Spot { get; set; }

        public double Volatility { get; set; }

        public double Rate { get; set; }

        public double Strike { get; set; }

        public DateTime SelectedMaturity { get; set; }

        public OptionType OptionType { get; set; }
    }

    public class PricerResponse
    {
        public string Symbol { get; set; }

        public double Spot { get; set; }

        public double Volatility { get; set; }

        public double Rate { get; set; }

        public double Strike { get; set; }

        public double Price { get; set; }

        public double Delta { get; set; }

        public double Gamma { get; set; }

        public OptionType OptionType { get; set; }

        public DateTime[] Maturities { get; set; }

        public DateTime SelectedMaturity { get; set; }
    }

    [ClientCanSwapTemplates]
    [DefaultView("Pricer")]
    public class PricerService : ServiceStack.ServiceInterface.Service
    {
        public IDateService DateService { get; set; }

        public IQuoteRepository QuoteRepository { get; set; }

        public object Get(PricerPage request)
        {
            var pricer = new Pricer { DateService = this.DateService, MarketDataService = new QuoteMarketDataService { QuoteRepository = QuoteRepository } };

            var maturities = GeneratesMaturities();
            
            int days = (int)maturities.First().Subtract(DateTime.Now).TotalDays;

            double spot = request.Symbol != null ? pricer.MarketDataService.GetSpot(request.Symbol) : 0.0;
            double vol = request.Symbol != null ? pricer.MarketDataService.GetHistoricalVolatility(request.Symbol, DateTime.Now, days) : 0.0;

            return new PricerResponse
            {
                Symbol = request.Symbol,
                Spot = spot,
                Maturities = maturities.ToArray(),
                SelectedMaturity = maturities.First(),
                Volatility = vol,
                OptionType = OptionType.Call,
                Rate = pricer.MarketDataService.GetInterestRate(),
                Strike = spot,
                Delta = 0.0,
                Gamma = 0.0,
                Price = 0.0
            };
        }

        public object Post(Price request)
        {
            var pricer = new Pricer { DateService = this.DateService, MarketDataService = new QuoteMarketDataService { QuoteRepository = QuoteRepository } };
            var maturities = GeneratesMaturities();

            int days = (int)request.SelectedMaturity.Subtract(DateTime.Now).TotalDays;

            var option = pricer.Price(request.OptionType, request.Symbol, request.SelectedMaturity, request.Strike, request.Spot, request.Volatility, request.Rate);

            return new PricerResponse
            {
                Symbol = request.Symbol,
                Spot = request.Spot,
                Maturities = maturities.ToArray(),
                SelectedMaturity = request.SelectedMaturity,
                Volatility = request.Volatility,
                OptionType = request.OptionType,
                Rate = request.Rate,
                Strike = request.Strike,
                Price = option.Price,
                Delta = option.Delta,
                Gamma = option.Gamma
            };
        }

        private IList<DateTime> GeneratesMaturities()
        {
            var maturityService = new MaturityService();
            var maturities = new List<DateTime>();
            var lastDate = DateTime.Now;
            for (int i = 0; i < 4; i++)
            {
                lastDate = maturityService.GetNextMaturityDay(lastDate.AddDays(1));
                maturities.Add(lastDate);
            }

            return maturities;
        }
    }
}
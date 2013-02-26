using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PricerTuto;

namespace ServiceStack.Tuto.WebHost.Repositories
{
    public class QuoteMarketDataService : IMarketDataService
    {
        public IQuoteRepository QuoteRepository { get; set; }

        public IList<HistoricalData> GetHistoricalData(string underlyingName, DateTime dateTime, int days)
        {
            var data = QuoteRepository.FindById(underlyingName);

            var result = new Dictionary<DateTime, HistoricalData>();
            
            DateTime firstDate = dateTime.AddDays(-days);

            foreach (var item in data.SpotHistory.Where(x => x.Date >= firstDate && x.Date <= dateTime).OrderBy(x => x.Date))
            {
                result[item.Date] = new HistoricalData { Date = item.Date, Close = item.Value };
            }

            foreach (var item in data.VolHistory.Where(x => x.Date >= firstDate && x.Date <= dateTime).OrderBy(x => x.Date))
            {
                HistoricalData historycalData = null;
                if (result.TryGetValue(item.Date, out historycalData))
                {
                    historycalData.Volatility = item.Value;
                }
            }

            return result.Values.ToList();
        }

        public double GetHistoricalVolatility(string underlyingName, DateTime dateTime, int days)
        {
            var historicalVolatilitySet = this.GetHistoricalData(underlyingName, dateTime, days);

            var statistics = new MathNet.Numerics.Statistics.DescriptiveStatistics(historicalVolatilitySet.Skip(1).Select(x => x.Volatility));

            // 252: open market days in a year
            return statistics.StandardDeviation * Math.Sqrt(252);
        }

        public double GetInterestRate()
        {
            return 0.01;
        }

        public IList<DateTime> GetListedMaturities(string underlying)
        {
            return new List<DateTime>();
        }

        public IList<double> GetListedStrikes(string underlying)
        {
            return new List<double>();
        }

        public IList<MarketOption> GetOptionData(string underlying, DateTime maturity)
        {
            return new List<MarketOption>();
        }

        public double GetSpot(string underlyingName)
        {
            return this.QuoteRepository.FindById(underlyingName).Value;
        }
    }
}
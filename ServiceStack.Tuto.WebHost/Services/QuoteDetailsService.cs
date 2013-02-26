using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.Tuto.WebHost.Repositories;
using ServiceStack.Tuto.WebHost.Model;

namespace ServiceStack.Tuto.WebHost.Services
{
    [Route("/quotedetails")]
    public class QuoteDetails
    {
        public string Symbol { get; set; }
    }

    public class QuoteDetailsResponse
    {
        public Quote Result { get; set; }

        public bool ShowMessage { get; set; }

        public bool IsMessageError { get; set; }

        public string Message { get; set; }
    }

    [ClientCanSwapTemplates]
    [DefaultView("QuoteDetails")]
    public class QuoteDetailsService : ServiceStack.ServiceInterface.Service
    {
        public IQuoteRepository QuoteRepository { get; set; }

        public PricerTuto.IMarketDataService MarketDataService { get; set; }

        public object Get(QuoteDetails request)
        {
            var found = QuoteRepository.FindById(request.Symbol);
            if (found != null)
            {
                return new QuoteDetailsResponse
                {
                    Result = found
                };
            }
            else
            {
                return new QuoteDetailsResponse
                {
                    Result = new Quote { Symbol = request.Symbol, Value = 0.0, LastUpdate = DateTime.Now },
                    IsMessageError = true,
                    ShowMessage = true,
                    Message = "Unknow symbol "+request.Symbol
                };
            }
        }

        public object Post(QuoteDetails request)
        {
            // find the underlying
            var quote = this.QuoteRepository.FindById(request.Symbol);

            try
            {
                // update the current spot
                quote.Value = MarketDataService.GetSpot(request.Symbol);
                quote.LastUpdate = DateTime.Now;

                // update the spot/vol history
                quote.SpotHistory = new List<Model.HistoryItem>();
                quote.VolHistory = new List<Model.HistoryItem>();

                var history = MarketDataService.GetHistoricalData(request.Symbol, DateTime.Now, 360);
                foreach (var item in history)
                {
                    var spotHistory = new HistoryItem
                    {
                        Date = item.Date,
                        Value = item.Close
                    };

                    var volHistory = new HistoryItem
                    {
                        Date = item.Date,
                        Value = item.Volatility
                    };

                    quote.SpotHistory.Add(spotHistory);
                    quote.VolHistory.Add(volHistory);
                }

                quote.SpotHistory = quote.SpotHistory.OrderBy(x => x.Date).ToList();
                quote.VolHistory = quote.VolHistory.OrderBy(x => x.Date).ToList();

                // save the underlying
                this.QuoteRepository.Save(quote);

                return new QuoteDetailsResponse
                {
                    Result = quote,
                    ShowMessage = true,
                    IsMessageError = false,
                    Message = "Successfully updated"
                };
            }
            catch (Exception ex)
            {
                return new QuoteDetailsResponse
                {
                    Result = quote,
                    ShowMessage = true,
                    IsMessageError = true,
                    Message = ex.Message
                };
            }
        }
    }
}
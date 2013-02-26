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
    [Route("/quotes")]
    public class Quotes
    {
    }

    [Route("/quotes/delete/{Symbol}")]
    public class DeleteQuote
    {
        public string Symbol { get; set; }
    }

    public class QuotesResponse
    {
        public int Total { get; set; }
        public List<Quote> Results { get; set; }
    }

    [ClientCanSwapTemplates]
    [DefaultView("Quotes")]
    public class QuotesService : ServiceStack.ServiceInterface.Service
    {
        public IQuoteRepository QuoteRepository { get; set; }

        public object Get(Quotes request)
        {
            var result = QuoteRepository.FindAll();
            return new QuotesResponse
            {
                Total = result.Count,
                Results = result
            };
        }

        public object Any(DeleteQuote request)
        {
            QuoteRepository.Delete(request.Symbol);
            return Get(new Quotes());
        }

        public object Post(Quote request)
        {
            QuoteRepository.Save(request);
            return Get(new Quotes());
        }
    }
}
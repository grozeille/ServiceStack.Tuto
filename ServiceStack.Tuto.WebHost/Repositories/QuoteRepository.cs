using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Concurrent;
using ServiceStack.Tuto.WebHost.Services;
using ServiceStack.Tuto.WebHost.Model;

namespace ServiceStack.Tuto.WebHost.Repositories
{
    public class QuoteRepository : ServiceStack.Tuto.WebHost.Repositories.IQuoteRepository
    {
        private readonly List<Quote> quotes = new List<Quote>();

        public QuoteRepository()
        {
            quotes.Add(new Quote { Symbol = "AAPL", Value = 519.33, LastUpdate = DateTime.Now });
            quotes.Add(new Quote { Symbol = "GOOG", Value = 715.63, LastUpdate = DateTime.Now });
            quotes.Add(new Quote { Symbol = "MSFT", Value = 27.45, LastUpdate = DateTime.Now });
            quotes.Add(new Quote { Symbol = "IBM", Value = 193.42, LastUpdate = DateTime.Now });
            quotes.Add(new Quote { Symbol = "ORCL", Value = 33.76, LastUpdate = DateTime.Now });
            quotes.Add(new Quote { Symbol = "TIBX", Value = 22.36, LastUpdate = DateTime.Now });
        }

        public Quote FindById(string symbol)
        {
            return quotes.Where(q => q.Symbol.Equals(symbol)).FirstOrDefault();
        }

        public List<Quote> FindAll()
        {
            return quotes.ToList();
        }

        public void Save(Quote quote)
        {
            Quote found = quotes.Where(q => q.Symbol.Equals(quote.Symbol)).FirstOrDefault();
            if (found != null)
            {
                quotes.Remove(found);
            }
            quotes.Add(quote);
        }

        public void Delete(string symbol)
        {
            Quote found = quotes.Where(q => q.Symbol.Equals(symbol)).FirstOrDefault();
            if (found != null)
            {
                quotes.Remove(found);
            }
        }
    }
}
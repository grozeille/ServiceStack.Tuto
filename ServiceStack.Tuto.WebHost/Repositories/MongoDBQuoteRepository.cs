using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Concurrent;
using ServiceStack.Tuto.WebHost.Services;
using ServiceStack.Tuto.WebHost.Model;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Builders;


namespace ServiceStack.Tuto.WebHost.Repositories
{
    public class MongoDBQuoteRepository : IQuoteRepository
    {
        private MongoCollection<Quote> quotes;

        public MongoDBQuoteRepository()
        {
            var server = new MongoServer(new MongoServerSettings{ Server = new MongoServerAddress("localhost") } );

            quotes = server.GetDatabase("pricer").GetCollection<Quote>("quote");

            if (quotes.Count() == 0)
            {
                quotes.Save(new Quote { Symbol = "AAPL", Value = 519.33, LastUpdate = DateTime.Now });
                quotes.Save(new Quote { Symbol = "GOOG", Value = 715.63, LastUpdate = DateTime.Now });
                quotes.Save(new Quote { Symbol = "MSFT", Value = 27.45, LastUpdate = DateTime.Now });
                quotes.Save(new Quote { Symbol = "IBM", Value = 193.42, LastUpdate = DateTime.Now });
                quotes.Save(new Quote { Symbol = "ORCL", Value = 33.76, LastUpdate = DateTime.Now });
                quotes.Save(new Quote { Symbol = "TIBX", Value = 22.36, LastUpdate = DateTime.Now });
            }
        }

        public Quote FindById(string symbol)
        {
            return quotes.AsQueryable<Quote>().Where(q => q.Symbol.Equals(symbol)).FirstOrDefault();
        }

        public List<Quote> FindAll()
        {
            return quotes.AsQueryable<Quote>().ToList();
        }

        public void Save(Quote quote)
        {
            quotes.Save(quote);
        }

        public void Delete(string symbol)
        {
            quotes.Remove(Query.EQ("_id", symbol));
        }
    }
}
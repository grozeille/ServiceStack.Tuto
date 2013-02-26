using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;
using ServiceStack.Tuto.Rest.Common;
using System.Collections.Concurrent;
using ServiceStack.ServiceClient.Web;
using ServiceStack.Tuto.Rest.Common.Repositories;
using ServiceStack.Tuto.Rest.Common.Operations;

namespace ServiceStack.Tuto.Rest.Common.Services
{
    public class QuoteService : ServiceStack.ServiceInterface.Service
    {
        public QuoteRepository QuoteRepository { get; set; }

        public List<Quote> Any(QuotesReq request)
        {
            return this.QuoteRepository.FindAll();
        }

        public Quote Get(QuoteReq request)
        {
            return this.QuoteRepository.FindById(request.Symbol);
        }

        public Quote Post(QuoteUpdateReq request)
        {
            var quote = new Quote { Symbol = request.Symbol, Value = request.Value, LastUpdate = DateTime.Now };
            this.QuoteRepository.Save(quote);
            return quote;
        }

        public void Delete(QuoteDeleteReq request)
        {
            this.QuoteRepository.Delete(request.Symbol);
        }
    }
}
using System;
using System.Collections.Generic;
using ServiceStack.Tuto.WebHost.Model;
namespace ServiceStack.Tuto.WebHost.Repositories
{
    public interface IQuoteRepository
    {
        void Delete(string symbol);

        List<Quote> FindAll();

        Quote FindById(string symbol);

        void Save(Quote quote);
    }
}

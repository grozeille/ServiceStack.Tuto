using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.Tuto.WebHost.Repositories;

namespace ServiceStack.Tuto.WebHost.Services
{
    [Route("/volhistory/{Symbol}")]
    public class VolHistory
    {
        public string Symbol { get; set; }
    }

    public class VolHistoryResponse
    {
        public long name { get; set; }

        public double y { get; set; }
    }

    public class VolHistoryService : ServiceStack.ServiceInterface.Service
    {
        public IQuoteRepository QuoteRepository { get; set; }

        public object Get(VolHistory request)
        {
            var result = QuoteRepository.FindById(request.Symbol);

            var response = new List<VolHistoryResponse>();

            if (result.VolHistory != null)
            {
                foreach (var item in result.VolHistory)
                {
                    response.Add(new VolHistoryResponse { name = item.Date.MillisecondsFromEpoch(), y = item.Value });
                }
            }

            return response;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.Tuto.WebHost.Repositories;

namespace ServiceStack.Tuto.WebHost.Services
{
    [Route("/spothistory/{Symbol}")]
    public class SpotHistory
    {
        public string Symbol { get; set; }
    }

    public class SpotHistoryResponse
    {
        public long name { get; set; }

        public double y { get; set; }
    }

    public class SpotHistoryService : ServiceStack.ServiceInterface.Service
    {
        public IQuoteRepository QuoteRepository { get; set; }

        public object Get(SpotHistory request)
        {
            var result = QuoteRepository.FindById(request.Symbol);

            var response = new List<SpotHistoryResponse>();

            if (result.SpotHistory != null)
            {
                foreach (var item in result.SpotHistory)
                {
                    response.Add(new SpotHistoryResponse { name = item.Date.MillisecondsFromEpoch(), y = item.Value });
                }
            }

            return response;
        }
    }
}
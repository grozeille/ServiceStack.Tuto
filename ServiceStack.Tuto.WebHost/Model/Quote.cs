using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;
using MongoDB.Bson.Serialization.Attributes;

namespace ServiceStack.Tuto.WebHost.Model
{
    [Route("/quotes", "POST")]
    public class Quote
    {
        [BsonId]
        public string Symbol { get; set; }

        public double Value { get; set; }

        public DateTime LastUpdate { get; set; }

        public List<HistoryItem> SpotHistory { get; set; }

        public List<HistoryItem> VolHistory { get; set; }
    }
}
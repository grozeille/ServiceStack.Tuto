using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.ServiceModel;
using FluentValidation;
using ServiceStack.ServiceInterface;

namespace ServiceStack.Tuto.Rest.Common.Operations
{
    [Route("/quotes", "GET")]
    public class QuotesReq : IReturn<List<Quote>> { }

    [Route("/quotes/{Symbol}", "GET")]
    public class QuoteReq : IReturn<Quote>
    {
        public string Symbol { get; set; }
    }

    [Authenticate]
    [RequiredRole("Admin")]
    [Route("/quotes/{Symbol}", "DELETE")]
    public class QuoteDeleteReq : IReturnVoid
    {
        public string Symbol { get; set; }
    }

    [Route("/quotes/{Symbol}", "POST")]
    public class QuoteUpdateReq : IReturn<Quote>
    {
        public string Symbol { get; set; }

        public double Value { get; set; }
    }
}

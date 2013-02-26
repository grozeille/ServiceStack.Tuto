using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceStack.Tuto.Rest.Common
{
    public class Quote
    {
        public string Symbol { get; set; }

        public double Value { get; set; }

        public DateTime LastUpdate { get; set; }
    }
}

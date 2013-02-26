using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;
using System.Runtime.Serialization;
using ServiceStack.ServiceInterface.ServiceModel;

namespace ServiceStack.Tuto.WebService.Common.Operations
{
    [DataContract]
    public class Add
    {
        [DataMember]
        public int A { get; set; }

        [DataMember]
        public int B { get; set; }
    }
}
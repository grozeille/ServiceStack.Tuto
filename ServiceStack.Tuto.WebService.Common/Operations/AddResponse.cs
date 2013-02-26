using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using ServiceStack.ServiceInterface.ServiceModel;

namespace ServiceStack.Tuto.WebService.Common.Operations
{
    [DataContract]
    public class AddResponse
    {
        /// <summary>
        /// If I put multiple fields here, the result will be "out" parameter in WCF client
        /// </summary>
        [DataMember]
        public AddResponseData Data { get; set; }

        public AddResponse()
        {
            this.Data = new AddResponseData();
        }
    }

    [DataContract]
    public class AddResponseData
    {
        [DataMember]
        public int Total { get; set; }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }

        public AddResponseData()
        {
            this.ResponseStatus = new ResponseStatus();
        }
    }
}

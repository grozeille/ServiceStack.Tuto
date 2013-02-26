using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.ServiceModel;
using ServiceStack.Tuto.WebService.Common.Operations;

namespace ServiceStack.Tuto.WebService.Services
{
    public class AddService : IService<Add>
    {
        public object Execute(Add request)
        {
            var response = new AddResponse();
            if (request.A == 0)
            {
                response.Data.ResponseStatus.ErrorCode = "500";
            }
            else
            {
                response.Data.Total = request.A + request.B;
            }

            return response;
        }
    }
}
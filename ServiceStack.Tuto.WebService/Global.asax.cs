using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using ServiceStack.WebHost.Endpoints;
using ServiceStack.Tuto.WebService.Common.Operations;
using ServiceStack.Tuto.WebService.Services;

namespace ServiceStack.Tuto.WebService
{
    public class Global : System.Web.HttpApplication
    {
        public class WebServiceAppHost : AppHostBase
        {
            //Tell Service Stack the name of your application and where to find your web services
            public WebServiceAppHost() : base("Web Services", typeof(AddService).Assembly) { }

            public override void Configure(Funq.Container container)
            {
                Routes.Add<Add>("/add");
                SetConfig(new EndpointHostConfig
                {
                    WsdlServiceNamespace = "http://grozeille.com/types",
                    WsdlSoapActionNamespace = "http://grozeille.com/types",
                });
            }
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            new WebServiceAppHost().Init();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}
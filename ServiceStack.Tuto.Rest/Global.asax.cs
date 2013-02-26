using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using ServiceStack.WebHost.Endpoints;
using Funq;
using ServiceStack.Tuto.Rest.Common.Repositories;
using ServiceStack.ServiceInterface.Admin;
using ServiceStack.ServiceInterface.Validation;
using ServiceStack.Tuto.Rest.Common.Operations;
using ServiceStack.Tuto.Rest.Common.Validators;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.CacheAccess;
using ServiceStack.CacheAccess.Providers;
using ServiceStack.Tuto.Rest.Common.Services;

namespace ServiceStack.Tuto.Rest
{
    public class Global : System.Web.HttpApplication
    {
        public class AppHost : AppHostHttpListenerBase
        {
            public AppHost() : base("REST", typeof(QuoteService).Assembly) { }

            public override void Configure(Container container)
            {
                container.Register<QuoteRepository>(new QuoteRepository());
                Plugins.Add(new RequestLogsFeature() { RequiredRoles = new string[0] });
                Plugins.Add(new ValidationFeature());
                Plugins.Add(new AuthFeature(() => new AuthUserSession(), new IAuthProvider[]
                {
                    new CredentialsAuthProvider(),         //HTML Form post of UserName/Password credentials
                    new BasicAuthProvider(),               //Sign-in with Basic Auth
                }));
                container.Register<ICacheClient>(new MemoryCacheClient());
                var userRep = new InMemoryAuthRepository();
                container.Register<IUserAuthRepository>(userRep);

                //Add a user for testing purposes
                string hash;
                string salt;
                new SaltedHash().GetHashAndSaltString("test", out hash, out salt);
                userRep.CreateUserAuth(new UserAuth
                {
                    Id = 1,
                    DisplayName = "DisplayName",
                    Email = "as@if.com",
                    UserName = "admin",
                    FirstName = "FirstName",
                    LastName = "LastName",
                    PasswordHash = hash,
                    Salt = salt,
                    Roles = new List<string>(new string[] { "Admin" })
                }, "test");

                container.RegisterValidators(typeof(QuoteUpdateReqValidator).Assembly);
            }
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            new AppHost().Init();
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
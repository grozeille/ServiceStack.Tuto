using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.ServiceClient.Web;
using ServiceStack.Tuto.Rest.Common;
using ServiceStack.Tuto.Rest.Common.Operations;
using ServiceStack.Common.ServiceClient.Web;
using ServiceStack.ServiceInterface.Auth;

namespace ServiceStack.Tuto.Rest.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new JsonServiceClient("http://localhost:50903/");

            // Async
            client.GetAsync<List<Quote>>(
                "/quotes/",
                quotes => 
                { 
                    foreach (var q in quotes) 
                    { 
                        Console.WriteLine("ALL: Quote {0} Value {1} UpdateDate {2}", q.Symbol, q.Value, q.LastUpdate); 
                    } 
                },
                (quotes, ex) => Console.WriteLine("Error: " + ex.ToString()));




            // Sync
            var quote = client.Get<Quote>(string.Format("/quotes/{0}", "GOOG"));
            Console.WriteLine("GET: Quote {0} Value {1} UpdateDate {2}", quote.Symbol, quote.Value, quote.LastUpdate);




            // Sync with typed request
            quote = client.Get<Quote>(new QuoteReq { Symbol = "GOOG" });
            Console.WriteLine("GET TYPED: Quote {0} Value {1} UpdateDate {2}", quote.Symbol, quote.Value, quote.LastUpdate);




            // Validation
            quote = client.Post<Quote>(new QuoteUpdateReq { Symbol = "GOOG", Value = -5 });
            Console.WriteLine("GET TYPED: Quote {0} Value {1} UpdateDate {2}", quote.Symbol, quote.Value, quote.LastUpdate);




            // Secure
            try
            {
                Console.WriteLine("Try delete without permissions");
                client.Delete(new QuoteDeleteReq { Symbol = "IBM" });
            }
            catch (Exception ex)
            {
                Console.WriteLine("No permissions: "+ex.ToString());
                var authResponse = client.Send<ServiceStack.ServiceInterface.Auth.AuthResponse>(new ServiceStack.ServiceInterface.Auth.Auth
                {
                    provider = CredentialsAuthProvider.Name,
                    UserName = "admin",
                    Password = "test",
                    RememberMe = true,  //important tell client to retain permanent cookies
                });

                Console.WriteLine("Try delete with permissions");
                client.Delete(new QuoteDeleteReq { Symbol = "IBM" });

                quote = client.Get<Quote>(new QuoteReq { Symbol = "IBM" });
                Console.WriteLine("Verify deleted: "+ (quote == null ? "deleted" : "not deleted"));
            }

            Console.ReadKey(true);
            
        }
    }
}

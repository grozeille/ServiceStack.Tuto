using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.ServiceClient.Web;
using ServiceStack.Tuto.WebService.Client.WcfServiceReference;
using ServiceStack.Tuto.WebService.Common.Operations;

namespace ServiceStack.Tuto.WebService.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            TestWcf();

            TestRest();

            TestAsyncRest();

            TestSoap12();

            TestSoap12Async();

            TestError();
            
            Console.ReadKey(true);
        }

        /// <summary>
        /// TestSoap12
        /// </summary>
        private static void TestSoap12()
        {
            var client = new Soap12ServiceClient("http://localhost:49301");

            var response = client.Send<AddResponse>(new Add { A = 1, B = 2 } );

            Console.WriteLine("Soap12 Total=" + response.Data.Total);
        }

        /// <summary>
        /// TestSoap12Async
        /// </summary>
        private static void TestSoap12Async()
        {
            var client = new Soap12ServiceClient("http://localhost:49301");

            try
            {
                client.SendAsync<AddResponse>(
                    new Add { A = 1, B = 2 },
                    response => Console.WriteLine("Soap12 Async Total=" + response.Data.Total),
                    (response, ex) => Console.WriteLine("Soap12 Async ERROR: " + ex.ToString()));
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.ToString());
            }
        }

        /// <summary>
        /// TestRest
        /// </summary>
        private static void TestRest()
        {
            var client = new JsonServiceClient("http://localhost:49301");

            var response = client.Send<AddResponse>(new Add { A = 1, B = 2 });

            Console.WriteLine("Json Total=" + response.Data.Total);
        }

        /// <summary>
        /// TestAsyncRest
        /// </summary>
        private static void TestAsyncRest()
        {
            var client = new JsonServiceClient("http://localhost:49301");

            client.SendAsync<AddResponse>(
                new Add { A = 1, B = 2 },
                    response => Console.WriteLine("Soap12 Total=" + response.Data.Total),
                    (response, ex) => Console.WriteLine("Json Async ERROR: " + ex.ToString()));
        }

        /// <summary>
        /// TestError
        /// </summary>
        private static void TestError()
        {
            var client = new Soap12ServiceClient("http://localhost:49301");
            
            try
            {
                var response = client.Send(new Add { A = 0, B = 2 } );
            }
            catch (WebServiceException ex)
            {
                Console.WriteLine("ERROR code: " + ex.ResponseStatus.ErrorCode);
            }
        }

        /// <summary>
        /// TestWcf
        /// </summary>
        private static void TestWcf()
        {
            var client = new ServiceStack.Tuto.WebService.Client.WcfServiceReference.SyncReplyClient();
            var result = client.Add(1, 2);
            if (result.ResponseStatus.Errors == null)
            {
                Console.WriteLine("Wcf Total=" + result.Total);
            }
            else
            {
                Console.WriteLine("Wcf Error:" + result.ResponseStatus.ToString());
            }
        }
    }
}

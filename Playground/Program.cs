using System;
using Amica.vNext;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Test().Wait();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
        }

        private static async Task Test()
        {
            var r = new Discovery()
            {
                BaseAddress = new Uri("http://10.0.2.2:9000/"),
            };
            var t = await r.GetApi(ApiKind.UserData);
            //var t = await r.GetService(ApiKind.UserData, ignoreCache:true);
            //var t = await r.GetServiceUri(ApiKind.Authentication, new Version("1.0"));


            //using (var client = new HttpClient { BaseAddress = new Uri("http://10.0.2.2:9000/") })
            //{
            //    client.DefaultRequestHeaders.Accept.Clear();
            //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //    client.DefaultRequestHeaders.Authorization = t.AuthenticationHeader();


            //    var res = await client.GetAsync("/countries");

            //}
        }
    }
}

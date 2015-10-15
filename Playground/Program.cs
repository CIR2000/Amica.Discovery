using System;
using Amica.vNext;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nito.AsyncEx;

namespace Playground
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                AsyncContext.Run(() => Test());
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
        }

        private static async Task Test()
        {
            var r = new Discovery();
            //var t = await r.GetApi(ApiKind.UserData);
            //var t = await r.GetServiceUri(ApiKind.UserData);
            var t = await r.GetServiceUri(ApiKind.UserData, new Version("1.0"));


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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GrpcConsoleApplication1
{
    class httpClient
    {
        public void Go()
        {

            HttpClient client = new HttpClient();

            var result = client.GetStringAsync("http://192.168.31.109").Result;
            Console.WriteLine(result);

        }
    }
}

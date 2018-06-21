using Grpc.Core;
using Helloworld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrpcConsoleApplication1
{
    public class grpcClient
    {
        private void Go()
        {
            Channel channel = new Channel("127.0.0.1:50051", ChannelCredentials.Insecure);

            var client = new Greeter.GreeterClient(channel);
            String user = "you";

            var reply = client.SayHello(new HelloRequest { Name = user });
            Console.WriteLine("Greeting: " + reply.Message);

            channel.ShutdownAsync().Wait();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        //private void Go()
        //{
        //    Channel channel = new Channel("127.0.0.1:50051", ChannelCredentials.Insecure);

        //    var client = new CC.CCClient(channel);
        //    String user = "you";

        //    var reply = client.SayCC(new CCRequest { Name = user });
        //    Console.WriteLine("Greeting: " + reply.Message);

        //    channel.ShutdownAsync().Wait();
        //    Console.WriteLine("Press any key to exit...");
        //    Console.ReadKey();
        //}


    }
}

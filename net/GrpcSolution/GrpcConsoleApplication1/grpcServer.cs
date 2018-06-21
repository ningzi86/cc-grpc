using Grpc.Core;
using Helloworld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrpcConsoleApplication1
{
    class GreeterImpl : Greeter.GreeterBase
    {
        // Server side handler of the SayHello RPC
        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            Console.WriteLine(String.Format("收到请求：{0}", request.Name));

            return Task.FromResult(new HelloReply { Message = "Hello " + request.Name });
        }
    }


    public class grpcServer
    {
        const int Port = 50051;

        public static void Main(String[] args)
        {
            Server server = new Server
            {
                Services = { Greeter.BindService(new GreeterImpl()) },
                Ports = { new ServerPort("localhost", Port, ServerCredentials.Insecure) }
            };
            server.Start();

            Console.WriteLine("Greeter server listening on port " + Port);
            Console.WriteLine("Press any key to stop the server...");
            Console.ReadKey();

            server.ShutdownAsync().Wait();
        }
    }
}

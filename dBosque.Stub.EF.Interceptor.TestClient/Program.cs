using System;
using System.Linq;

namespace dBosque.Stub.EF.Interceptor.TestClient
{
    class Program

    {
        static void Main(string[] args)
        {
            using (var context = new StubDbEntities())
            {
                var message = context.MessageType.FirstOrDefault();
                Console.WriteLine($"{message.Namespace}.{message.Rootnode}");
                System.Threading.Thread.Sleep(4000);
                message = context.MessageType.FirstOrDefault();
                Console.WriteLine($"{message.Namespace}.{message.Rootnode}");

                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
            }
        }
    }
}
;
using dBosque.Stub.Client.Socket.Process;
using System;

namespace dBosque.Stub.Client.Socket.TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("TestClient <host> <port>");
                return;
            }

            try
            {
                using (var proxy = new StubServerConnection(args[0], Int32.Parse(args[1])))
                {
                    while (true)
                    {
                        Console.WriteLine("Enter text and press enter.");
                        string msg = Console.ReadLine();
                        var res = proxy.Send(msg, out int? error);
                        Console.WriteLine($"=>{error}:{res}");
                    }
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

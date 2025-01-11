using CachingProxyServer;
using CachingProxyCLI.Validations;

namespace CachingProxyCLI.Program
{
    public static class Program
    {
        static async Task Main(string[] args)
        {
            if(!CommandLineValidator.ValidateArguments(args))
            {
                return;
            }

            ProxyServer proxyServer = new ProxyServer(Int32.Parse(args[1]), args[3]);
            var serverTask = Task.Run(() => proxyServer.Start());
            bool exitLoop = false;
            while(!exitLoop)
            {
                Console.WriteLine("Entering the loop");
                string? userinput = Console.ReadLine();
                switch (userinput)
                {
                    case "--clear-cache":
                        proxyServer.ClearCache();
                        break;
                    case "--exit":
                        proxyServer.Stop();
                        exitLoop = true;
                        break;
                    default:
                        continue;
                }
            }

            await serverTask;
        }
    }
}
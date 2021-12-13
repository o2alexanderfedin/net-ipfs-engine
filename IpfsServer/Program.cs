using System;
using System.Threading;
using System.Threading.Tasks;
using Ipfs.Engine;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace Ipfs.Server
{
    /// <summary>
    ///   Manages an IPFS server.
    /// </summary>
    public class Program
    {
        static CancellationTokenSource cancel = new CancellationTokenSource();
        /// <summary>
        ///   The IPFS Core API engine.
        /// </summary>
        public static IpfsEngine IpfsEngine;
        const string passphrase = "this is not a secure pass phrase";

        /// <summary>
        ///   Main entry point.
        /// </summary>
        public static async Task Main(string[] args)
        {
            try
            {
                IpfsEngine = new IpfsEngine(passphrase.ToCharArray());
                await IpfsEngine.StartAsync();

                await BuildWebHost(args)
                    .RunAsync(cancel.Token);
            }
            catch (TaskCanceledException)
            {
                // eat it
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message); // TODO: better error handling
            }

            if (IpfsEngine != null)
            {
                await IpfsEngine.StopAsync();
            }
        }

        static IWebHost BuildWebHost(string[] args)
        {
            var urls = "http://127.0.0.1:5009";
            var addrTask = Task.Run(async () =>
            {
                var jToken = await IpfsEngine.Config.GetAsync("Addresses.API");
                return jToken;
            });
            var addr = (string)addrTask.Result;
            if (addr != null)
            {
                // Quick and dirty: multiaddress to URL
                urls = addr
                    .Replace("/ip4/", "http://")
                    .Replace("/ip6/", "http://")
                    .Replace("/tcp/", ":");
            }

            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                })
                .UseUrls(urls)
                .Build();
        }

        /// <summary>
        ///   Stop the program.
        /// </summary>
        public static void Shutdown()
        {
            cancel.Cancel();
        }
    }
}

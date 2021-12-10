using Common.Logging;
using Common.Logging.Simple;
using Ipfs.Engine;
using System.Threading.Tasks;

namespace Spike
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // set logger factory
            var properties = new Common.Logging.Configuration.NameValueCollection();
            properties["level"] = "DEBUG";
            properties["showLogName"] = "true";
            properties["showDateTime"] = "true";
            properties["dateTimeFormat"] = "HH:mm:ss.fff";
            LogManager.Adapter = new ConsoleOutLoggerFactoryAdapter(properties);

            var test = new BitswapApiTest();
            await test.GetsBlock_OnConnect();
        }
    }
}
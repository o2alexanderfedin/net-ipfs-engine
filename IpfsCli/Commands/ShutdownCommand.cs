using McMaster.Extensions.CommandLineUtils;
using System.Threading.Tasks;

namespace Ipfs.Cli
{
    [Command(Description = "Stop the IPFS deamon")]
    class ShutdownCommand : CommandBase
    {
        Program Parent { get; set; }

        protected override async Task<int> OnExecute(CommandLineApplication app)
        {
            await Parent.CoreApi.Generic.ShutdownAsync();
            return 0;
        }
    }
}

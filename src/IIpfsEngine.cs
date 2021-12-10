using System;
using Ipfs.CoreApi;
using Nito.AsyncEx;
using PeerTalk;

namespace Ipfs.Engine
{
    /// <summary>
    /// Common interface for <see cref="IpfsEngine"/>
    /// </summary>
    public interface IIpfsEngine : ICoreApi, IService, IDisposable
    {
        /// <summary>
        ///   Manages communication with other peers.
        /// </summary>
        AsyncLazy<Swarm> SwarmService { get; }

        /// <summary>
        ///   Manages publishng and subscribing to messages.
        /// </summary>
        AsyncLazy<PeerTalk.PubSub.NotificationService> PubSubService { get; }

        /// <summary>
        ///   Exchange blocks with other peers.
        /// </summary>
        AsyncLazy<BlockExchange.Bitswap> BitswapService { get; }

        /// <summary>
        ///   Finds information with a distributed hash table.
        /// </summary>
        AsyncLazy<PeerTalk.Routing.Dht1> DhtService { get; }

        /// <summary>
        ///   Determines latency to a peer.
        /// </summary>
        AsyncLazy<PeerTalk.Protocols.Ping1> PingService { get; }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Features;
using MithrilShards.Core.Network.Protocol;
using MithrilShards.Core.Network.Protocol.Processors;

namespace MithrilShards.Core.Network {
   public interface IPeerContext : IDisposable {
      /// <summary>
      /// Gets the direction of the peer connection.
      /// </summary>
      PeerConnectionDirection Direction { get; }

      /// <summary>
      /// Gets the peer identifier.
      /// </summary>
      string PeerId { get; }

      /// <summary>
      /// Gets the local peer end point.
      /// </summary>
      IPEndPoint LocalEndPoint { get; }

      /// <summary>External IP address and port number used to access the node from external network.</summary>
      /// <remarks>Used to announce to external peers the address they connect to in order to reach our Forge server.</remarks>
      IPEndPoint PublicEndPoint { get; }

      /// <summary>
      /// Gets the remote peer end point.
      /// </summary>
      IPEndPoint RemoteEndPoint { get; }

      PeerMetrics Metrics { get; }

      /// <summary>
      /// Gets the version peers agrees to use when their respective version doesn't match.
      /// It should be the lower common version both parties implements.
      /// </summary>
      /// <value>
      /// The negotiated protocol version.
      /// </value>
      INegotiatedProtocolVersion NegotiatedProtocolVersion { get; }

      /// <summary>
      /// Generic container to exchange content between different components that share IPeerContext.
      /// </summary>
      IFeatureCollection Data { get; }

      /// <summary>
      /// Gets the message writer used to send a message to the peer.
      /// </summary>
      /// <returns></returns>
      INetworkMessageWriter GetMessageWriter();


      /// <summary>
      /// Attaches the network message processor to the peer context.
      /// </summary>
      /// <param name="messageProcessor">The message processor to attach.</param>
      void AttachNetworkMessageProcessor(INetworkMessageProcessor messageProcessor);

      /// <summary>
      /// Processes the message asynchronously, calling all the attached <see cref="INetworkMessageProcessor"/>.
      /// </summary>
      /// <param name="message">The network message.</param>
      /// <returns></returns>
      Task ProcessMessageAsync(INetworkMessage message);

      CancellationTokenSource ConnectionCancellationTokenSource { get; }
   }
}
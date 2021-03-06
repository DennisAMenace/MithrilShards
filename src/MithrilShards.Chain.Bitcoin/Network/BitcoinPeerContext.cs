﻿using System;
using System.Net;
using Microsoft.Extensions.Logging;
using MithrilShards.Core.Network;
using MithrilShards.Core.Network.Protocol;
using MithrilShards.Core.Network.Protocol.Processors;

namespace MithrilShards.Chain.Bitcoin.Network {
   public class BitcoinPeerContext : PeerContext {

      public TimeSpan? TimeOffset { get; set; }

      public BitcoinPeerContext(ILogger logger,
                                PeerConnectionDirection direction,
                                string peerId,
                                EndPoint localEndPoint,
                                EndPoint publicEndPoint,
                                EndPoint remoteEndPoint,
                                INetworkMessageWriter messageWriter)
         : base(logger, direction, peerId, localEndPoint, publicEndPoint, remoteEndPoint, messageWriter) {
      }

      public override void AttachNetworkMessageProcessor(INetworkMessageProcessor messageProcessor) {
         base.AttachNetworkMessageProcessor(messageProcessor);
      }
   }
}

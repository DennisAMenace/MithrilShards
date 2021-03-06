﻿using MithrilShards.Core.Network.Protocol;

namespace MithrilShards.Core.Network.Events {
   /// <summary>
   /// A peer message has been received and parsed
   /// </summary>
   /// <seealso cref="Stratis.Bitcoin.EventBus.EventBase" />
   public class PeerMessageReceived : PeerEventBase {
      public INetworkMessage Message { get; }

      public int MessageSize { get; }

      public PeerMessageReceived(IPeerContext peerContext, INetworkMessage message, int messageSize) : base(peerContext) {
         this.Message = message;
         this.MessageSize = messageSize;
      }
   }
}
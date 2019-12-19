﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MithrilShards.Core;
using MithrilShards.Core.EventBus;
using MithrilShards.P2P.Network.StateMachine;

namespace MithrilShards.P2P.Network {

   public class PeerConnection : IPeerConnection {
      /// <summary>Instance logger.</summary>
      private readonly ILogger logger;
      readonly IEventBus eventBus;

      /// <summary>Provider of time functions.</summary>
      private readonly IDateTimeProvider dateTimeProvider;
      internal TcpClient ConnectedClient { get; }
      public PeerConnectionDirection Direction { get; }

      readonly CancellationToken cancellationToken;

      public Guid PeerConnectionId { get; }

      readonly PeerConnectionStateMachine connectionStateMachine;

      public TimeSpan? TimeOffset { get; private set; }

      public PeerDisconnectionReason DisconnectReason { get; private set; }

      public PeerConnection(ILogger<PeerConnection> logger,
                            IEventBus eventBus,
                            IDateTimeProvider dateTimeProvider,
                            TcpClient connectedClient,
                            PeerConnectionDirection peerConnectionDirection,
                            CancellationToken cancellationToken) {
         this.logger = logger;
         this.eventBus = eventBus;
         this.dateTimeProvider = dateTimeProvider;
         this.ConnectedClient = connectedClient;
         this.Direction = peerConnectionDirection;
         this.cancellationToken = cancellationToken;

         this.PeerConnectionId = Guid.NewGuid();

         this.connectionStateMachine = new PeerConnectionStateMachine(logger, eventBus, this, cancellationToken);
      }

      /// <inheritdoc/>
      public async Task IncomingConnectionAccepted(CancellationToken cancellation = default(CancellationToken)) {
         try {
            await this.connectionStateMachine.AcceptIncomingConnection().ConfigureAwait(false);
         }
         catch (Exception ex) {
            this.logger.LogCritical(ex, "Unexpected error.");
            this.connectionStateMachine.ForceDisconnection();
         }
      }
   }
}

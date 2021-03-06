﻿using System;
using Microsoft.Extensions.Logging;
using MithrilShards.Core.Extensions;

namespace MithrilShards.Core.EventBus {
   /// <summary>
   /// Default implementation of <see cref="ISubscriptionErrorHandler"/> that log the error and re-throw it.
   /// </summary>
   /// <seealso cref="ISubscriptionErrorHandler" />
   public class DefaultSubscriptionErrorHandler : ISubscriptionErrorHandler {
      /// <summary>
      /// The logger
      /// </summary>
      private readonly ILogger logger;

      public DefaultSubscriptionErrorHandler(ILogger<DefaultSubscriptionErrorHandler> logger) {
         this.logger = logger;
      }

      /// <inheritdoc />
      public void Handle(EventBase @event, Exception exception, ISubscription subscription) {
         this.logger.LogError(exception, "Error handling the event {0}", @event.GetType().Name);
         throw exception;
      }
   }
}

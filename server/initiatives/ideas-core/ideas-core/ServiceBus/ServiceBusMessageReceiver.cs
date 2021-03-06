﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CoE.Ideas.Shared.ServiceBus;
using EnsureThat;
using Microsoft.Azure.ServiceBus;
using Serilog;

namespace CoE.Ideas.Core.ServiceBus
{
    internal class ServiceBusMessageReceiver : IMessageReceiver
    {
        public ServiceBusMessageReceiver(ISubscriptionClient subscriptionClient,
            ILogger logger)
        {
            EnsureArg.IsNotNull(subscriptionClient);
            EnsureArg.IsNotNull(logger);
            _subscriptionClient = subscriptionClient;
            _logger = logger;
        }
        private readonly ISubscriptionClient _subscriptionClient;
        private readonly ILogger _logger;


        public Task AbandonAsync(string lockToken, IDictionary<string, object> propertiesToModify = null)
        {
            return _subscriptionClient.AbandonAsync(lockToken, propertiesToModify);
        }

        public Task CompleteAsync(string lockToken)
        {
            return _subscriptionClient.CompleteAsync(lockToken);
        }


        public Task DeadLetterAsync(string lockToken, string deadLetterReason, string deadLetterErrorDescription = null)
        {
            return _subscriptionClient.DeadLetterAsync(lockToken, deadLetterReason, deadLetterErrorDescription);
        }

        public void RegisterMessageHandler(Func<Shared.ServiceBus.Message, CancellationToken, Task> handler, MessageHandlerOptions messageHandlerOptions)
        {
            _subscriptionClient.RegisterMessageHandler(async (msg, token) =>
            {
                _logger.Debug("Received service bus message {MessageId}: {Label}", msg.MessageId, msg.Label);

                // transofrm msg to Message
                var messageDto = new Shared.ServiceBus.Message()
                {
                    Id = Guid.Parse(msg.MessageId),
                    Label = msg.Label,
                    MessageProperties = msg.UserProperties,
                    CreatedDateUtc = msg.SystemProperties.EnqueuedTimeUtc,
                    LockToken = msg.SystemProperties.LockToken
                };
                await handler(messageDto, token);
            }, messageHandlerOptions);
        }

    }
}

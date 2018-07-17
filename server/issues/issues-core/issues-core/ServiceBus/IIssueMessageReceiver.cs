﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;


namespace CoE.Issues.Core.ServiceBus
{
    public interface IIssueMessageReceiver
    {
        void ReceiveMessages(
            Func<IssueCreatedEventArgs, CancellationToken, Task> issueCreatedHandler = null,
            MessageHandlerOptions options = null
            );
    }
}
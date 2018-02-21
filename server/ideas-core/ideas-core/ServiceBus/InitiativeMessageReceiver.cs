﻿using CoE.Ideas.Core.Security;
using CoE.Ideas.Core.WordPress;
using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoE.Ideas.Core.ServiceBus
{
    //TODO: Mark messages as complete, abadoned or dead lettered!

    public class InitiativeMessageReceiver : IInitiativeMessageReceiver
    {
        public InitiativeMessageReceiver(IIdeaRepository repository,
            IWordPressClient wordPressClient,
            ISubscriptionClient subscriptionClient,
            IJwtTokenizer jwtTokenizer,
            Serilog.ILogger logger)
        {
            _repository = repository ?? throw new ArgumentNullException("repository");
            _wordPressClient = wordPressClient ?? throw new ArgumentNullException("wordPressClient");
            _subscriptionClient = subscriptionClient ?? throw new ArgumentNullException("subscriptionClient");
            _jwtTokenizer = jwtTokenizer ?? throw new ArgumentNullException("jwtTokenizer");
            _logger = logger ?? throw new ArgumentNullException("logger");
        }
        private readonly IIdeaRepository _repository;
        private readonly IWordPressClient _wordPressClient;
        private readonly ISubscriptionClient _subscriptionClient;
        private readonly IJwtTokenizer _jwtTokenizer;
        private readonly Serilog.ILogger _logger;

        private IDictionary<string, ICollection<Func<Message, CancellationToken, Task>>> MessageMap = new Dictionary<string, ICollection<Func<Message, CancellationToken, Task>>>();

        public void ReceiveMessages(Func<InitiativeCreatedEventArgs, CancellationToken, Task> initiativeCreatedHndler = null,
            Func<WorkOrderCreatedEventArgs, CancellationToken, Task> workOrderCreatedHandler = null, 
            Func<WorkOrderUpdatedEventArgs, CancellationToken, Task> workOrderUpdatedHandler = null,
            Func<InitiativeLoggedEventArgs, CancellationToken, Task> initiativeLoggedHandler = null,
            MessageHandlerOptions options = null)
        {
            MessageHandlerOptions messageHandlerOptions = options ?? new MessageHandlerOptions(OnDefaultError);
            messageHandlerOptions.AutoComplete = false;
            _subscriptionClient.RegisterMessageHandler(async (msg, token) =>
            {
                switch (msg.Label)
                {
                    // TODO: These should be moved to a constants file
                    case "Initiative Created":
                    {
                        if (initiativeCreatedHndler != null)
                            await ReceiveInitiativeCreated(msg, token, initiativeCreatedHndler);
                        else
                            await _subscriptionClient.CompleteAsync(msg.SystemProperties.LockToken);
                        break;
                    }
                    case "Remedy Work Item Created":
                        if (workOrderCreatedHandler != null)
                            await ReceiveInitiativeWorkItemCreated(msg, token, workOrderCreatedHandler);
                        else
                            await _subscriptionClient.CompleteAsync(msg.SystemProperties.LockToken);
                        break;
                    case "Work Order Updated":
                        if (workOrderUpdatedHandler != null)
                            await ReceiveWorkOrderUpdated(msg, token, workOrderUpdatedHandler);
                        else
                            await _subscriptionClient.CompleteAsync(msg.SystemProperties.LockToken);
                        break;
                    case "Initiative Logged":
                        if (initiativeLoggedHandler != null)
                            await ReceiveInitiativeLogged(msg, token, initiativeLoggedHandler);
                        else
                            await _subscriptionClient.CompleteAsync(msg.SystemProperties.LockToken);
                        break;
                    default:
                    {
                        await _subscriptionClient.DeadLetterAsync(msg.SystemProperties.LockToken, $"Unknown message type: { msg.Label }");
                        break;
                    }
                }
            }, messageHandlerOptions);
        }

        protected virtual Task OnDefaultError(ExceptionReceivedEventArgs err)
        {
            _logger.Error(err.Exception, "Error receiving message: {ErrorMessage}", err.Exception.Message);
            return Task.CompletedTask;
        }

        protected virtual async Task ReceiveInitiativeCreated(Message msg, CancellationToken token, Func<InitiativeCreatedEventArgs, CancellationToken, Task> handler)
        {
            if (msg == null)
                throw new ArgumentNullException("msg");
            if (handler == null)
                throw new ArgumentNullException("handler");

            if (await EnsureMessageLabel(msg, "Initiative Created"))
            {
                var idea = await GetMessageInitiative(msg);
                if (idea.WasMessageDeadLettered)
                    return;
                var owner = await GetMessageOwner(msg);
                if (owner.WasMessageDeadLettered)
                    return;

                try
                {
                    await handler(new InitiativeCreatedEventArgs()
                    {
                        Initiative = idea.Item,
                        Owner = owner.Item
                    }, token);
                    await _subscriptionClient.CompleteAsync(msg.SystemProperties.LockToken);
                }
                catch (Exception err)
                {
                    System.Diagnostics.Trace.TraceWarning($"InitiativeCreated handler threw the following error, abandoning message for future processing: { err.Message }");
                    await _subscriptionClient.AbandonAsync(msg.SystemProperties.LockToken);
                }
            }
        }

        protected virtual async Task ReceiveInitiativeWorkItemCreated(Message msg, CancellationToken token, Func<WorkOrderCreatedEventArgs, CancellationToken, Task> handler)
        {
            if (msg == null)
                throw new ArgumentNullException("msg");
            if (handler == null)
                throw new ArgumentNullException("handler");

            if (await EnsureMessageLabel(msg, "Remedy Work Item Created"))
            {
                var idea = await GetMessageInitiative(msg);
                if (idea.WasMessageDeadLettered)
                    return;
                var owner = await GetMessageOwner(msg);
                if (owner.WasMessageDeadLettered)
                    return;
                var workOrderId = await GetMessageString(msg, propertyName: "WorkOrderId");
                if (workOrderId.WasMessageDeadLettered)
                    return;

                try
                {
                    await handler(new WorkOrderCreatedEventArgs()
                    {
                        Initiative = idea.Item,
                        Owner = owner.Item,
                        WorkOrderId = workOrderId.Item
                    }, token);
                    await _subscriptionClient.CompleteAsync(msg.SystemProperties.LockToken);
                }
                catch (Exception err)
                {
                    System.Diagnostics.Trace.TraceWarning($"InitiativeWorkItemCreated handler threw the following error, abandoning message for future processing: { err.Message }");
                    await _subscriptionClient.AbandonAsync(msg.SystemProperties.LockToken);
                }
            }
        }

        protected virtual async Task ReceiveWorkOrderUpdated(Message msg, CancellationToken token, Func<WorkOrderUpdatedEventArgs, CancellationToken, Task> handler)
        {
            if (msg == null)
                throw new ArgumentNullException("msg");
            if (handler == null)
                throw new ArgumentNullException("handler");

            if (await EnsureMessageLabel(msg, "Work Order Updated"))
            {
                var updatedStatus = await GetMessageString(msg, propertyName: "WorkOrderStatus");
                if (updatedStatus.WasMessageDeadLettered)
                    return;
                var workOrderId = await GetMessageString(msg, propertyName: "WorkOrderId");
                if (workOrderId.WasMessageDeadLettered)
                    return;
                var updateTime = await GetMessageProperty<DateTime>(msg, propertyName: "WorkOrderUpdateTimeUtc");
                if (updateTime.WasMessageDeadLettered)
                    return;
                var assigneeEmail = await GetMessageString(msg, propertyName: "WorkOrderAssigneeEmail", allowNullOrEmptyString: true);
                var assigneeDisplayname = await GetMessageString(msg, propertyName: "WorkOrderAssigneeDisplayName", allowNullOrEmptyString: true);

                try
                {
                    await handler(new WorkOrderUpdatedEventArgs()
                    {
                        UpdatedStatus = updatedStatus.Item,
                        UpdatedDateUtc = updateTime.Item,
                        WorkOrderId = workOrderId.Item,
                        AssigneeEmail = assigneeEmail.Item,
                        AssigneeDisplayName = assigneeDisplayname.Item
                    }, token);
                    await _subscriptionClient.CompleteAsync(msg.SystemProperties.LockToken);
                }
                catch (Exception err)
                {
                    System.Diagnostics.Trace.TraceWarning($"WorkOrderUpdated handler threw the following error, abandoning message for future processing: { err.Message }");
                    await _subscriptionClient.AbandonAsync(msg.SystemProperties.LockToken);
                }
            }
        }

        protected virtual async Task ReceiveInitiativeLogged(Message msg, CancellationToken token, Func<InitiativeLoggedEventArgs, CancellationToken, Task> handler)
        {
            if (msg == null)
                throw new ArgumentNullException("msg");
            if (handler == null)
                throw new ArgumentNullException("handler");

            if (await EnsureMessageLabel(msg, "Initiative Logged"))
            {
                var idea = await GetMessageInitiative(msg);
                if (idea.WasMessageDeadLettered)
                    return;
                var owner = await GetMessageOwner(msg);
                if (owner.WasMessageDeadLettered)
                    return;
                var rangeUpdated = await GetMessageString(msg, propertyName: "RangeUpdated");
                if (rangeUpdated.WasMessageDeadLettered)
                    return;

                try
                {
                    await handler(new InitiativeLoggedEventArgs()
                    {
                        Initiative = idea.Item,
                        Owner = owner.Item,
                        RangeUpdated = rangeUpdated.Item
                    }, token);
                }
                catch (Exception err)
                {
                    System.Diagnostics.Trace.TraceWarning($"InitiativeLogged handler threw the following error, abandoning message for future processing: { err.Message }");
                    await _subscriptionClient.AbandonAsync(msg.SystemProperties.LockToken);
                }
            }
        }


        private async Task<bool> EnsureMessageLabel(Message message, string label)
        {
            if (message.Label == label)
                return true;
            else
            {
                await _subscriptionClient.DeadLetterAsync(message.SystemProperties.LockToken, $"Label was unexpected. Expected '{ label }', got '{ message.Label }';");
                return false;
            }
        }


        protected virtual async Task<GetItemResult<Idea>> GetMessageInitiative(Message message)
        {
            if (message == null)
                throw new ArgumentNullException("msg");

            var initiativeIdResult = await GetMessageProperty<long>(message, propertyName: "InitiativeId");
            var result = new GetItemResult<Idea>();
            if (initiativeIdResult.WasMessageDeadLettered)
            {
                result.SetMessageDeadLettered(initiativeIdResult.Errors.FirstOrDefault());
            }
            else
            {
                try
                {
                    result.Item = await _repository.GetIdeaAsync(initiativeIdResult.Item);
                }
                catch (Exception err)
                {
                    string errorMessage = $"Unable to get Initiative { initiativeIdResult.Item }: { err.Message }";
                    result.SetMessageDeadLettered(errorMessage);
                    await _subscriptionClient.DeadLetterAsync(message.SystemProperties.LockToken, errorMessage);

                }

            }

            return result;
        }

        protected virtual async Task<GetItemResult<ClaimsPrincipal>> GetMessageOwner(Message message)
        {
            if (message == null)
                throw new ArgumentNullException("msg");

            var ownerTokenResult = await GetMessageString(message, propertyName: "OwnerToken");
            var result = new GetItemResult<ClaimsPrincipal>();
            if (ownerTokenResult.WasMessageDeadLettered)
            {
                result.SetMessageDeadLettered(ownerTokenResult.Errors.FirstOrDefault());
            }
            else
            {

                try
                {
                    result.Item = _jwtTokenizer.CreatePrincipal(ownerTokenResult.Item);
                }
                catch (Exception err)
                {
                    string errorMessage = $"Unable to get Owner from token { ownerTokenResult.Item }: { err.Message }";
                    result.SetMessageDeadLettered(errorMessage);
                    await _subscriptionClient.DeadLetterAsync(message.SystemProperties.LockToken, errorMessage);
                }
            }

            // last fail safe
            if (!result.WasMessageDeadLettered && result.Item == null)
            {
                string errorMessage = $"Unable to get Owner, reason unknown";
                result.SetMessageDeadLettered(errorMessage);
                await _subscriptionClient.DeadLetterAsync(message.SystemProperties.LockToken, errorMessage);
            }

            return result;
        }

        protected virtual async Task<GetItemResult<string>> GetMessageString(Message message, string propertyName, bool allowNullOrEmptyString = false)
        {
            if (message == null)
                throw new ArgumentNullException("msg");

            var result = await GetMessageProperty<string>(message, propertyName: propertyName, allowNull: allowNullOrEmptyString);
            if (!result.WasMessageDeadLettered && !allowNullOrEmptyString)
            {
                if (string.IsNullOrWhiteSpace(result.Item))
                {
                    string errorMessage = $"{ propertyName } was empty";
                    result.SetMessageDeadLettered(errorMessage);
                    await _subscriptionClient.DeadLetterAsync(message.SystemProperties.LockToken, errorMessage);
                }
            }
            return result;
        }

        protected virtual async Task<GetItemResult<T>> GetMessageProperty<T>(Message message, string propertyName, bool allowNull = false) 
        {
            if (message == null)
                throw new ArgumentNullException("msg");

            var result = new GetItemResult<T>();
            if (!message.UserProperties.ContainsKey(propertyName))
            {
                string errorMessage = $"{ propertyName } not found in message";
                result.SetMessageDeadLettered(errorMessage);
                await _subscriptionClient.DeadLetterAsync(message.SystemProperties.LockToken, errorMessage);
            }
            else
            {
                object propertyObj = message.UserProperties[propertyName];
                if (propertyObj == null)
                {
                    if (!allowNull)
                    {
                        string errorMessage = $"{ propertyName } was null";
                        result.SetMessageDeadLettered(errorMessage);
                        await _subscriptionClient.DeadLetterAsync(message.SystemProperties.LockToken, errorMessage);
                    }
                    // else return null (or default) and don't dead letter
                }
                else
                {
                    try
                    {
                        result.Item = (T)propertyObj;
                    }
                    catch (Exception)
                    {
                        string errorMessage = $"{ propertyName } was not of type { typeof(T).FullName }";
                        result.SetMessageDeadLettered(errorMessage);
                        await _subscriptionClient.DeadLetterAsync(message.SystemProperties.LockToken, errorMessage);
                    }
                }
            }

            return result;
        }

        public class GetItemResult<T>
        {
            private readonly Message message;

            public GetItemResult()
            {
                errorsList = new List<string>();
                WasMessageDeadLettered = false; // until set by SetMessageDeadLettered()
            }
            public T Item { get; set; }
            private IList<string> errorsList;
            public IEnumerable<string> Errors { get { return errorsList; } }
            public void SetMessageDeadLettered(string reason)
            {
                WasMessageDeadLettered = true;
                errorsList.Add(reason);
            }
            public bool WasMessageDeadLettered { get; private set; }
        }


    }
}

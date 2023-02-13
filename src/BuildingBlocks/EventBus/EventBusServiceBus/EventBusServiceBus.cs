#region Corpspace© Apache-2.0
// Copyright © 2023 Sultan Soltanov. All rights reserved.
// Author: Sultan Soltanov
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion

using Corpspace.BuildingBlocks.EventBus;
using Corpspace.BuildingBlocks.EventBus.Abstractions;
using Corpspace.BuildingBlocks.EventBus.Events;

namespace Corpspace.BuildingBlocks.EventBusServiceBus;

public class EventBusServiceBus : IEventBus, IAsyncDisposable
{
    private readonly IServiceBusPersisterConnection _serviceBusPersisterConnection;
    private readonly ILogger<EventBusServiceBus> _logger;
    private readonly IEventBusSubscriptionsManager _subsManager;
    private readonly ILifetimeScope _autofac;
    private const string TopicName = "csp_event_bus";
    private readonly string _subscriptionName;
    private readonly ServiceBusSender _sender;
    private readonly ServiceBusProcessor _processor;
    private const string AutofacScopeName = "csp_event_bus";
    private const string IntegrationEventSuffix = "IntegrationEvent";

    public EventBusServiceBus(IServiceBusPersisterConnection serviceBusPersisterConnection,
        ILogger<EventBusServiceBus> logger, IEventBusSubscriptionsManager subsManager, ILifetimeScope autofac, string subscriptionClientName)
    {
        _serviceBusPersisterConnection = serviceBusPersisterConnection;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _subsManager = subsManager ?? new InMemoryEventBusSubscriptionsManager();
        _autofac = autofac;
        _subscriptionName = subscriptionClientName;
        _sender = _serviceBusPersisterConnection.TopicClient.CreateSender(TopicName);
        var options = new ServiceBusProcessorOptions { MaxConcurrentCalls = 10, AutoCompleteMessages = false };
        _processor = _serviceBusPersisterConnection.TopicClient.CreateProcessor(TopicName, _subscriptionName, options);

        RemoveDefaultRule();
        RegisterSubscriptionClientMessageHandlerAsync().GetAwaiter().GetResult();
    }

    public void Publish(IntegrationEvent @event)
    {
        var eventName = @event.GetType().Name.Replace(IntegrationEventSuffix, "");
        var jsonMessage = JsonSerializer.Serialize(@event, @event.GetType());
        var body = Encoding.UTF8.GetBytes(jsonMessage);

        var message = new ServiceBusMessage
        {
            MessageId = Guid.NewGuid().ToString(),
            Body = new BinaryData(body),
            Subject = eventName,
        };

        _sender.SendMessageAsync(message)
            .GetAwaiter()
            .GetResult();
    }

    public void SubscribeDynamic<Th>(string eventName)
        where Th : IDynamicIntegrationEventHandler
    {
        _logger.LogInformation("Subscribing to dynamic event {EventName} with {EventHandler}", eventName, typeof(Th).Name);

        _subsManager.AddDynamicSubscription<Th>(eventName);
    }

    public void Subscribe<T, Th>()
        where T : IntegrationEvent
        where Th : IIntegrationEventHandler<T>
    {
        var eventName = typeof(T).Name.Replace(IntegrationEventSuffix, "");

        var containsKey = _subsManager.HasSubscriptionsForEvent<T>();
        if (!containsKey)
        {
            try
            {
                _serviceBusPersisterConnection.AdministrationClient.CreateRuleAsync(TopicName, _subscriptionName, new CreateRuleOptions
                {
                    Filter = new CorrelationRuleFilter() { Subject = eventName },
                    Name = eventName
                }).GetAwaiter().GetResult();
            }
            catch (ServiceBusException)
            {
                _logger.LogWarning("The messaging entity {eventName} already exists.", eventName);
            }
        }

        _logger.LogInformation("Subscribing to event {EventName} with {EventHandler}", eventName, typeof(Th).Name);

        _subsManager.AddSubscription<T, Th>();
    }

    public void Unsubscribe<T, Th>()
        where T : IntegrationEvent
        where Th : IIntegrationEventHandler<T>
    {
        var eventName = typeof(T).Name.Replace(IntegrationEventSuffix, "");

        try
        {
            _serviceBusPersisterConnection
                .AdministrationClient
                .DeleteRuleAsync(TopicName, _subscriptionName, eventName)
                .GetAwaiter()
                .GetResult();
        }
        catch (ServiceBusException ex) when (ex.Reason == ServiceBusFailureReason.MessagingEntityNotFound)
        {
            _logger.LogWarning("The messaging entity {eventName} Could not be found.", eventName);
        }

        _logger.LogInformation("Unsubscribing from event {EventName}", eventName);

        _subsManager.RemoveSubscription<T, Th>();
    }

    public void UnsubscribeDynamic<Th>(string eventName)
        where Th : IDynamicIntegrationEventHandler
    {
        _logger.LogInformation("Unsubscribing from dynamic event {EventName}", eventName);

        _subsManager.RemoveDynamicSubscription<Th>(eventName);
    }

    private async Task RegisterSubscriptionClientMessageHandlerAsync()
    {
        _processor.ProcessMessageAsync +=
            async (args) =>
            {
                var eventName = $"{args.Message.Subject}{IntegrationEventSuffix}";
                string messageData = args.Message.Body.ToString();

                // Complete the message so that it is not received again.
                if (await ProcessEvent(eventName, messageData))
                {
                    await args.CompleteMessageAsync(args.Message);
                }
            };

        _processor.ProcessErrorAsync += ErrorHandler;
        await _processor.StartProcessingAsync();
    }

    private Task ErrorHandler(ProcessErrorEventArgs args)
    {
        var ex = args.Exception;
        var context = args.ErrorSource;

        _logger.LogError(ex, "ERROR handling message: {ExceptionMessage} - Context: {@ExceptionContext}", ex.Message, context);

        return Task.CompletedTask;
    }

    private async Task<bool> ProcessEvent(string eventName, string message)
    {
        var processed = false;
        if (_subsManager.HasSubscriptionsForEvent(eventName))
        {
            var scope = _autofac.BeginLifetimeScope(AutofacScopeName);
            var subscriptions = _subsManager.GetHandlersForEvent(eventName);
            foreach (var subscription in subscriptions)
            {
                if (subscription.IsDynamic)
                {
                    if (scope.ResolveOptional(subscription.HandlerType) is not IDynamicIntegrationEventHandler handler) continue;

                    using dynamic eventData = JsonDocument.Parse(message);
                    await handler.Handle(eventData);
                }
                else
                {
                    var handler = scope.ResolveOptional(subscription.HandlerType);
                    if (handler == null) continue;
                    var eventType = _subsManager.GetEventTypeByName(eventName);
                    var integrationEvent = JsonSerializer.Deserialize(message, eventType);
                    var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);
                    await (Task)concreteType.GetMethod("Handle").Invoke(handler, new[] { integrationEvent });
                }
            }
        }
        processed = true;
        return processed;
    }

    private void RemoveDefaultRule()
    {
        try
        {
            _serviceBusPersisterConnection
                .AdministrationClient
                .DeleteRuleAsync(TopicName, _subscriptionName, RuleProperties.DefaultRuleName)
                .GetAwaiter()
                .GetResult();
        }
        catch (ServiceBusException ex) when (ex.Reason == ServiceBusFailureReason.MessagingEntityNotFound)
        {
            _logger.LogWarning("The messaging entity {DefaultRuleName} Could not be found.", RuleProperties.DefaultRuleName);
        }
    }

    public async ValueTask DisposeAsync()
    {
        _subsManager.Clear();
        await _processor.CloseAsync();
    }
}
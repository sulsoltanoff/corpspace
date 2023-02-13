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

using Corpspace.BuildingBlocks.EventBus.Abstractions;
using Corpspace.BuildingBlocks.EventBus.Events;

namespace Corpspace.BuildingBlocks.EventBus;

public partial class InMemoryEventBusSubscriptionsManager : IEventBusSubscriptionsManager
{


    private readonly Dictionary<string, List<SubscriptionInfo>> _handlers;
    private readonly List<Type> _eventTypes;

    public event EventHandler<string> OnEventRemoved;

    public InMemoryEventBusSubscriptionsManager()
    {
        _handlers = new Dictionary<string, List<SubscriptionInfo>>();
        _eventTypes = new List<Type>();
    }

    public bool IsEmpty => _handlers is { Count: 0 };
    public void Clear() => _handlers.Clear();

    public void AddDynamicSubscription<Th>(string eventName)
        where Th : IDynamicIntegrationEventHandler
    {
        DoAddSubscription(typeof(Th), eventName, isDynamic: true);
    }

    public void AddSubscription<T, Th>()
        where T : IntegrationEvent
        where Th : IIntegrationEventHandler<T>
    {
        var eventName = GetEventKey<T>();

        DoAddSubscription(typeof(Th), eventName, isDynamic: false);

        if (!_eventTypes.Contains(typeof(T)))
        {
            _eventTypes.Add(typeof(T));
        }
    }

    private void DoAddSubscription(Type handlerType, string eventName, bool isDynamic)
    {
        if (!HasSubscriptionsForEvent(eventName))
        {
            _handlers.Add(eventName, new List<SubscriptionInfo>());
        }

        if (_handlers[eventName].Any(s => s.HandlerType == handlerType))
        {
            throw new ArgumentException(
                $"Handler Type {handlerType.Name} already registered for '{eventName}'", nameof(handlerType));
        }

        _handlers[eventName]
            .Add(isDynamic ? SubscriptionInfo.Dynamic(handlerType) : SubscriptionInfo.Typed(handlerType));
    }


    public void RemoveDynamicSubscription<Th>(string eventName)
        where Th : IDynamicIntegrationEventHandler
    {
        var handlerToRemove = FindDynamicSubscriptionToRemove<Th>(eventName);
        DoRemoveHandler(eventName, handlerToRemove);
    }


    public void RemoveSubscription<T, Th>()
        where Th : IIntegrationEventHandler<T>
        where T : IntegrationEvent
    {
        var handlerToRemove = FindSubscriptionToRemove<T, Th>();
        var eventName = GetEventKey<T>();
        DoRemoveHandler(eventName, handlerToRemove);
    }


    private void DoRemoveHandler(string eventName, SubscriptionInfo subsToRemove)
    {
        if (subsToRemove != null)
        {
            _handlers[eventName].Remove(subsToRemove);
            if (!_handlers[eventName].Any())
            {
                _handlers.Remove(eventName);
                var eventType = _eventTypes.SingleOrDefault(e => e.Name == eventName);
                if (eventType != null)
                {
                    _eventTypes.Remove(eventType);
                }
                RaiseOnEventRemoved(eventName);
            }

        }
    }

    public IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>() where T : IntegrationEvent
    {
        var key = GetEventKey<T>();
        return GetHandlersForEvent(key);
    }
    public IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName) => _handlers[eventName];

    private void RaiseOnEventRemoved(string eventName)
    {
        var handler = OnEventRemoved;
        handler?.Invoke(this, eventName);
    }


    private SubscriptionInfo FindDynamicSubscriptionToRemove<Th>(string eventName)
        where Th : IDynamicIntegrationEventHandler
    {
        return DoFindSubscriptionToRemove(eventName, typeof(Th));
    }


    private SubscriptionInfo FindSubscriptionToRemove<T, Th>()
            where T : IntegrationEvent
            where Th : IIntegrationEventHandler<T>
    {
        var eventName = GetEventKey<T>();
        return DoFindSubscriptionToRemove(eventName, typeof(Th));
    }

    private SubscriptionInfo DoFindSubscriptionToRemove(string eventName, Type handlerType)
    {
        if (!HasSubscriptionsForEvent(eventName))
        {
            return null;
        }

        return _handlers[eventName].SingleOrDefault(s => s.HandlerType == handlerType);

    }

    public bool HasSubscriptionsForEvent<T>() where T : IntegrationEvent
    {
        var key = GetEventKey<T>();
        return HasSubscriptionsForEvent(key);
    }
    public bool HasSubscriptionsForEvent(string eventName) => _handlers.ContainsKey(eventName);

    public Type GetEventTypeByName(string eventName) => _eventTypes.SingleOrDefault(t => t.Name == eventName);

    public string GetEventKey<T>()
    {
        return typeof(T).Name;
    }
}

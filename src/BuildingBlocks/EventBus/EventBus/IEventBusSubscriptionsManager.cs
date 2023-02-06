#region Corpspace© Apache-2.0
// Copyright 2023 The Corpspace Technologies
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

public interface IEventBusSubscriptionsManager
{
    bool IsEmpty { get; }
    event EventHandler<string> OnEventRemoved;
    void AddDynamicSubscription<Th>(string eventName)
        where Th : IDynamicIntegrationEventHandler;

    void AddSubscription<T, Th>()
        where T : IntegrationEvent
        where Th : IIntegrationEventHandler<T>;

    void RemoveSubscription<T, Th>()
            where Th : IIntegrationEventHandler<T>
            where T : IntegrationEvent;
    void RemoveDynamicSubscription<Th>(string eventName)
        where Th : IDynamicIntegrationEventHandler;

    bool HasSubscriptionsForEvent<T>() where T : IntegrationEvent;
    bool HasSubscriptionsForEvent(string eventName);
    Type GetEventTypeByName(string eventName);
    void Clear();
    IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>() where T : IntegrationEvent;
    IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName);
    string GetEventKey<T>();
}
